using AgentsProject.Interfaces;
using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Algorithms
{
    public class BasicAlgorithm : IAlgorithm
    {

        public bool EnterAuction(Guid auctionID, AuctionDeatiels auctionDeatiels)
        {
            return true;
        }

        public double? FirstOffer(Guid auctionID, AuctionDeatiels auctionDeatiels)
        {
            return auctionDeatiels.StartPrice + auctionDeatiels.PriceJump;
        }


        public double? NewOffer(Guid auctionID, string agentName, double offerPrice, AuctionDeatiels auctionDeatiels)
        {
            if (offerPrice < 500)
            {
                return offerPrice + auctionDeatiels.PriceJump;
            }

            return null;
        }

        public double? OfferLastChance(Guid auctionID, string agentName, double offerPrice, AuctionDeatiels auctionDeatiels)
        {
            return null;
        }
    }
}
