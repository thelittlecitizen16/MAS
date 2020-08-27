using AgentsProject.Interfaces;
using Common;
using MAS.Interfaces;
using MAS.MasDB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MAS.AuctionManagement
{
    public class ManageAuction
    {
        public ManageAuctionWithAgents ManageAuctionAgents;
        private ISystem _system;
        private ConsoleColor _color;
        private ManageProducts _manageProducts;
        private ManageAgents _manageAgents;
        private AuctionDeatiels _auctionDeatiels;
        private List<IAgent> _allAgentsInAuction;

        public ManageAuction(ConsoleColor color, ManageProducts manageProducts, ManageAgents manageAgents, ManageAuctionWithAgents manageAuctionWithAgents, ISystem system)
        {
            ManageAuctionAgents = manageAuctionWithAgents;
            _system = system;
            _color = color;
            _manageProducts = manageProducts;
            _manageAgents = manageAgents;
            _allAgentsInAuction = new List<IAgent>();
            _auctionDeatiels = new AuctionDeatiels(ManageAuctionAgents.Auction.Name, ManageAuctionAgents.Auction.StartPrice, ManageAuctionAgents.Auction.PriceJump);
        }
        public bool SendAboutNewAuction()
        {
            List<Task> tasks = new List<Task>(); ;
            bool HaveOne = false;
            foreach (var agent in _manageAgents.AllAgents)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    if (agent.EnterAuction(ManageAuctionAgents.Auction.ID, _auctionDeatiels))
                    {
                        ManageAuctionAgents.Subscribe(agent);
                        _allAgentsInAuction.Add(agent);
                        HaveOne = true;
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            return HaveOne;
        }
        public void StartAuction()
        {
            string auctionName = ManageAuctionAgents.Auction.Name;
            string productName = ManageAuctionAgents.Auction.Product.Name;
            double startPrice = ManageAuctionAgents.Auction.StartPrice;
            _system.Write($"the auction {auctionName} start now- the product is {productName} the start price is {startPrice}", _color);
        }
    
        public void EndAuction()
        {
            ManageAuctionAgents.LastAgentOffer.TakeMoneyWhenWin(ManageAuctionAgents.LastOfferPrice);

            _system.Write($"{ManageAuctionAgents.LastAgentOffer.Name} is the winner, SOLD {ManageAuctionAgents.Auction.Product.Name} for {ManageAuctionAgents.LastOfferPrice}", _color);
            
            _manageProducts.RemoveProduct(ManageAuctionAgents.Auction.Product);

            ManageAuctionAgents.SendAgentAboutEndAuction();
            ManageAuctionAgents.RemoveAllAgentsFromEvent(_allAgentsInAuction);
        }

        public void SendLastChance()
        {
            _system.Write($"{ManageAuctionAgents.LastAgentOffer.Name} is the last offer with price {ManageAuctionAgents.LastOfferPrice}", _color);
        }

        public void CheckOffer(List<Tuple<double?, IAgent>> allResults)
        {
            allResults = allResults.Where(r => r != null).ToList();
            foreach (var result in allResults)
            {

                if (result.Item1.HasValue)
                {
                    if (ManageAuctionAgents.LastOfferPrice < result.Item1 && IsJumpOk(result.Item1.Value))
                    {
                        ManageAuctionAgents.LastOfferPrice = result.Item1.Value;
                        ManageAuctionAgents.LastAgentOffer = result.Item2;
                    }
                }

            }
        }

        public bool IsJumpOk(double price)
        {
            if (price >= ManageAuctionAgents.LastOfferPrice + ManageAuctionAgents.Auction.PriceJump)
            {
                return true;
            }

            return false;
        }
    }
}
