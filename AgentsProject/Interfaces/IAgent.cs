using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Interfaces
{
    public interface IAgent
    {
        string Name { get; }
        ConcurrentDictionary<Guid, AuctionDeatiels> AuctionsDeatiels { get; }
        double MoneyAccount { get; }
        bool EnterAuction(Guid auctionID, AuctionDeatiels auctionDeatiels);
        Tuple<double?, IAgent> FirstOffer(Guid auctionID);
        Tuple<double?, IAgent> NewOffer(Guid auctionID,string agentName, double offerPrice);
        Tuple<double?, IAgent> OfferLastChance(Guid auctionID, string agentName, double offerPrice);
    }
}
