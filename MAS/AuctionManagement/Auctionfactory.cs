using MAS.Interfaces;
using MAS.MasDB;
using MAS.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.AuctionManagement
{
    public class Auctionfactory
    {
        private Random rand;
        public Auctionfactory()
        {
            rand = new Random();
        }
        public RunAuction CreateAuction(ISystem system, ManageProducts manageProducts, ManageAgents manageAgents, string name, DateTime startTime, TimeSpan waitWithoutOffer, IProduct product, double startPrice, double priceJump)
        {
            int color =rand.Next(1, 16);

            Auction auction = new Auction(name, startTime, waitWithoutOffer, product, startPrice, priceJump);
            ManageAuctionWithAgents manageAuctionWithAgents = new ManageAuctionWithAgents(auction);
            ManageAuction manageAuction = new ManageAuction((ConsoleColor)color, manageProducts, manageAgents, manageAuctionWithAgents, system);
            
            return  new RunAuction(manageAuction, manageAgents, system, (ConsoleColor)color);
        }
    }
}
