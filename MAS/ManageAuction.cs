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
            Console.WriteLine($"the auction over-");
            Console.WriteLine($"{_lastAgentOffer.Name} is the winner and he buy in {_lastOfferPrice}");

        }

        public List<Tuple<double?, IAgent>> SendAgentIfWantToAddFirstOffer()
        {
            var parameter = new object[] { Auction.ID };
            return SendAgentAboutOffer(true,parameter);
        }

        public List<Tuple<double?, IAgent>> SendAgentIfWantToAddNewOffer()
        {
            var parameters = new object[] { Auction.ID, _lastAgentOffer.Name , _lastOfferPrice};
            return SendAgentAboutOffer(false,parameters);
        }

        private List<Tuple<double?, IAgent>> SendAgentAboutOffer(bool first, params object[] paramaters)
        {
            List<Task> tasks = new List<Task>();

            List<Tuple<double?, IAgent>> allResults = new List<Tuple<double?, IAgent>>();
            var allAgentsInAuctions = NewOffer.GetInvocationList();

            if (first)
            {
                 allAgentsInAuctions = FirstOffer.GetInvocationList();
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
                    if (_lastOfferPrice < result.Item1)
                    {
                        _lastOfferPrice = result.Item1.Value;
                        _lastAgentOffer = result.Item2;
                    }
                }
                
            }
        }
    }
}
