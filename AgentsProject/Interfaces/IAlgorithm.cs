using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Interfaces
{
    public interface IAlgorithm
    {
        bool EnterAuction(Guid auctionID, AuctionDeatiels auctionDeatiels);
        double? FirstOffer(Guid auctionID, AuctionDeatiels auctionDeatiels);
        double? NewOffer(Guid auctionID, string agentName, double offerPrice, AuctionDeatiels auctionDeatiels);
        double? OfferLastChance(Guid auctionID, string agentName, double offerPrice, AuctionDeatiels auctionDeatiels);
    }
}
