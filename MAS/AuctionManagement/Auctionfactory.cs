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
        public RunAuction CreateAuction(ISystem system,ManageAgents manageAgents, string name, DateTime startTime, TimeSpan waitWithoutOffer, IProduct product, double startPrice, double priceJump)
        {
            Auction auction = new Auction(name, startTime, waitWithoutOffer, product, startPrice, priceJump);
            ManageAuction manageAuction = new ManageAuction(auction, system);
            return  new RunAuction(manageAuction, manageAgents, system);
        }
    }
}
