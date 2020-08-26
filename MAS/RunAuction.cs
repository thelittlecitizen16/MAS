using AgentsProject.Interfaces;
using MAS.NewFolder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MAS
{
    public class RunAuction
    {
        private ManageAuction _manageAuction;
        private ManageAgents _manageAgents;

        public RunAuction(ManageAuction manageAuction)
        {
            _manageAgents = new ManageAgents();
            _manageAuction = manageAuction;
        }
        public  void SendAboutNewAuction()
        {
            foreach (var agent in _manageAgents.AllAgents)
            {
                Task.Factory.StartNew(() =>
                {
                    if (agent.EnterAuction(_manageAuction.Auction.Name, _manageAuction.Auction.StartPrice, _manageAuction.Auction.PriceJump))
                    {
                        _manageAuction.Subscribe(agent);
                    }
                });
            }
        }


        public void Run()
        {
            _manageAuction.SendIfWantToAddFirstOffer();
        }

    }
}
