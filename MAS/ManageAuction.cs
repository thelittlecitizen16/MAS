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


    }
}
