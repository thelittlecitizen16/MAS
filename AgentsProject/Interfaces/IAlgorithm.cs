using System;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Interfaces
{
    public interface IAlgorithm
    {
        bool EnterAuction(Guid auctionID, AuctionDeatiels auctionDeatiels);
        double FirstOffer(Guid auctionID);
        double? NewOffer(Guid auctionID, string agentName, double offerPrice);
        double? OfferLastChance(Guid auctionID, string agentName, double offerPrice);
    }
}
