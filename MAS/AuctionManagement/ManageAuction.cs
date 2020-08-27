using AgentsProject.Interfaces;
using MAS.Interfaces;
using MAS.MasDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MAS.AuctionManagement
{
    public class ManageAuction
    {
        public Auction Auction;
        private ISystem _system;
        private ConsoleColor _color;
        private IAgent _lastAgentOffer;
        private double _lastOfferPrice;
        private ManageProducts _manageProducts;
        private event Func<Guid, Tuple<double?, IAgent>> FirstOffer;
        private event Func<Guid, string, double, Tuple<double?, IAgent>> NewOffer;
        private event Func<Guid, string, double, Tuple<double?, IAgent>> LastOffer;
        private event Action<Guid> EndAuctionToAgent;
        //  private object Locker = new object();

        public ManageAuction(ConsoleColor color, ManageProducts manageProducts, Auction auction, ISystem system)
        {
            Auction = auction;
            _system = system;
            _color = color;
            _manageProducts = manageProducts;
            _lastOfferPrice = 0;
        }
        public void Subscribe(IAgent agent)
        {
            //  lock (Locker)
            //{
            SendIfWantToAddFirstOffer(agent);
            SendIfWantToAddNewOffer(agent);
            LastChance(agent);
            EventEndAuction(agent);
            // }

        }

        public void StartAuction()
        {
            _system.Write($"the auction {Auction.Name} start now- the product is {Auction.Product.Name} the start price is {Auction.StartPrice}", _color);
        }

        private void SendIfWantToAddFirstOffer(IAgent agent)
        {
            FirstOffer += agent.FirstOffer;
        }

        private void SendIfWantToAddNewOffer(IAgent agent)
        {
            NewOffer += agent.NewOffer;
        }

        private void LastChance(IAgent agent)
        {
            LastOffer += agent.OfferLastChance;
        }

        private void EventEndAuction(IAgent agent)
        {
            EndAuctionToAgent += agent.EndAuction;
        }

        public void EndAuction()
        {
            _lastAgentOffer.TakeMoneyWhenWin(_lastOfferPrice);
            _system.Write($"{_lastAgentOffer.Name} is the winner, SOLD {Auction.Product.Name} for {_lastOfferPrice}", _color);
            _manageProducts.RemoveProduct(Auction.Product);
            SendAgentAboutEndAuction();
        }

        public void SendLastChance()
        {
            _system.Write($"{_lastAgentOffer.Name} is the last offer with price {_lastOfferPrice}", _color);
        }

        public List<Tuple<double?, IAgent>> SendAgentIfWantToAddFirstOffer()
        {
            var parameter = new object[] { Auction.ID };
            return SendAgentAboutOffer(FirstOffer.GetInvocationList(), parameter);
        }

        public List<Tuple<double?, IAgent>> SendAgentIfWantToAddNewOffer()
        {
            var parameters = new object[] { Auction.ID, _lastAgentOffer.Name, _lastOfferPrice };
            return SendAgentAboutOffer(NewOffer.GetInvocationList(), parameters);
        }

        public List<Tuple<double?, IAgent>> SendAgentIfWantToAddLastOffer()
        {
            var parameters = new object[] { Auction.ID, _lastAgentOffer.Name, _lastOfferPrice };
            return SendAgentAboutOffer(LastOffer.GetInvocationList(), parameters);
        }

        public List<Tuple<double?, IAgent>> SendAgentAboutEndAuction()
        {
            var parameters = new object[] { Auction.ID};
            return SendAgentAboutOffer(EndAuctionToAgent.GetInvocationList(), parameters);
        }
         
        private List<Tuple<double?, IAgent>> SendAgentAboutOffer(Delegate[] allAgentsInAuctions,  params object[] paramaters)
        {
            List<Task> tasks = new List<Task>();

            List<Tuple<double?, IAgent>> allResults = new List<Tuple<double?, IAgent>>();

            foreach (var agent in allAgentsInAuctions)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    Tuple<double?, IAgent> tuple = (Tuple<double?, IAgent>)agent?.DynamicInvoke(paramaters);
                    allResults.Add(tuple);
                }));
            }

            Task.WaitAll(tasks.ToArray());

            return allResults;
        }

        public void CheckOffer(List<Tuple<double?, IAgent>> allResults)
        {
            allResults = allResults.Where(r => r != null).ToList();
            foreach (var result in allResults)
            {

                if (result.Item1.HasValue)
                {
                    if (_lastOfferPrice < result.Item1 && IsJumpOk(result.Item1.Value))
                    {
                        _lastOfferPrice = result.Item1.Value;
                        _lastAgentOffer = result.Item2;
                    }
                }

            }
        }

        public bool IsJumpOk(double price)
        {
            if (price >= _lastOfferPrice + Auction.PriceJump)
            {
                return true;
            }

            return false;
        }
    }
}
