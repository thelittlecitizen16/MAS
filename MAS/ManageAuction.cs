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
        private List<IAgent> agentInAuction;

        public ManageAuction(Auction auction)
        {
            _auction = auction;
            _manageAgents = new ManageAgents();
        }

        public void SendAboutNewAuction()
        {
            foreach (var agent in _manageAgents.AllAgents)
            {
                Task.Factory.StartNew(() =>
                {
                    if (agent.EnterAuction(_auction.Name, _auction.StartPrice, _auction.PriceJump))
                    {
                        agentInAuction.Add(agent);
                    }                   
                });
            }  
        }
    }
}
