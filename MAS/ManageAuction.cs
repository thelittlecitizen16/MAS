using AgentsProject.Interfaces;
using MAS.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MAS
{
    public class ManageAuction
    {
        public Auction Auction;
        private IAgent _lastAgentOffer;
        private double _lastOfferPrice;
        private event Func<Guid,Tuple<double?, IAgent>> FirstOffer;
        private event Func<Guid, string, double, Tuple<double?, IAgent>> NewOffer;
        private event Func<Guid, string, double, Tuple<double?, IAgent>> LastOffer;

        public ManageAuction(Auction auction)
        {
            Auction = auction;
        }
        public void Subscribe(IAgent agent)
        {
            SendIfWantToAddFirstOffer(agent);
            SendIfWantToAddNewOffer(agent);
            LastChance(agent);
        }

        public void StartAuction()
        {
            Console.WriteLine($"the auction {Auction.Name} start now-");
            Console.WriteLine($" the product is {Auction.Product.Name} the start price is {Auction.StartPrice}");
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

        public void EndAuction()
        {
            Console.WriteLine($"SOLD for {_lastOfferPrice} - {Auction.Product.Name} ");
            Console.WriteLine($"{_lastAgentOffer.Name} is the winner!!");

        }
        public void SendLastChance()
        {
            Console.WriteLine($"{_lastAgentOffer.Name} is the last offer with price {_lastOfferPrice}");
        }

        public List<Tuple<double?, IAgent>> SendAgentIfWantToAddFirstOffer()
        {
            var parameter = new object[] { Auction.ID };
            return SendAgentAboutOffer(0,parameter);
        }

        public List<Tuple<double?, IAgent>> SendAgentIfWantToAddNewOffer()
        {
            var parameters = new object[] { Auction.ID, _lastAgentOffer.Name , _lastOfferPrice};
            return SendAgentAboutOffer(1,parameters);
        }
        public List<Tuple<double?, IAgent>> SendAgentIfWantToAddLastOffer()
        {
            var parameters = new object[] { Auction.ID, _lastAgentOffer.Name, _lastOfferPrice };
            return SendAgentAboutOffer(2, parameters);
        }

        private List<Tuple<double?, IAgent>> SendAgentAboutOffer(int place, params object[] paramaters)
        {
            List<Task> tasks = new List<Task>();

            List<Tuple<double?, IAgent>> allResults = new List<Tuple<double?, IAgent>>();
            var allAgentsInAuctions = FirstOffer.GetInvocationList();

            if (place==1)
            {
                 allAgentsInAuctions = NewOffer.GetInvocationList();

            }
            if (place == 2)
            {
                 allAgentsInAuctions = LastOffer.GetInvocationList();

            }


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

        public void AddFirstOffer(List<Tuple<double?, IAgent>> allResults)
        {
            _lastAgentOffer = allResults.First().Item2;
            _lastOfferPrice = allResults.First().Item1.Value;
        }
        public void CheckOffer(List<Tuple<double?, IAgent>> allResults)
        {
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
