using AgentsProject.Interfaces;
using MAS.NewFolder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MAS
{
    public class ManageAuction
    {
        private Auction _auction;
        private ManageAgents _manageAgents;
        private List<IAgent> _agentInAuction;
        private IAgent _lastAgentOffer;
        private double _lastOfferPrice;
        private event Func<double> FirstOffer;
        private event Func<string, double,  double> NewOffer;
        private event Func<string, double, double> LastOffer;


        public ManageAuction(Auction auction)
        {
            _auction = auction;
            _manageAgents = new ManageAgents();
            _agentInAuction = new List<IAgent>();
        }

        public void SendAboutNewAuction()
        {
            foreach (var agent in _manageAgents.AllAgents)
            {
                Task.Factory.StartNew(() =>
                {
                    if (agent.EnterAuction(_auction.Name, _auction.StartPrice, _auction.PriceJump))
                    {
                        _agentInAuction.Add(agent);
                    }                   
                });
            }  
        }

        public void StartAuction()
        {
            Console.WriteLine($"the auction {_auction.Name} start now-");
            Console.WriteLine($" the product is {_auction.Product} the start price is {_auction.StartPrice}");
        }

        public void SendIfWantToAddFirstOffer(IAgent agent)
        {
            FirstOffer += agent.FirstOffer;
        }

        public void SendIfWantToAddNewOffer(IAgent agent)
        {
            NewOffer += agent.NewOffer;
        }

        public void LastChance(IAgent agent)
        {
            LastOffer += agent.OfferLastChance;
        }

        public void EndAuction(IAgent agent)
        {
            Console.WriteLine($"the auction over-");
            Console.WriteLine($"{_lastAgentOffer.Name} is the winner and he buy in {_lastOfferPrice}");

        }
    }
}
