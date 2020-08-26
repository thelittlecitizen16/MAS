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
        private ConcurrentDictionary<Guid, AuctionDeatiels> _auctionsDeatiels;

        public BasicAlgorithm()
        {
            _auctionsDeatiels = new ConcurrentDictionary<Guid, AuctionDeatiels>();
        }

        public bool EnterAuction(Guid auctionID, AuctionDeatiels auctionDeatiels)
        {
            _auctionsDeatiels.TryAdd(auctionID, auctionDeatiels);
            return true;
        }

        public double? FirstOffer(Guid auctionID)
        {
            return _auctionsDeatiels[auctionID].StartPrice + _auctionsDeatiels[auctionID].PriceJump;
        }


        public double? NewOffer(Guid auctionID, string agentName, double offerPrice)
        {
            if (offerPrice < 500)
            {
                return offerPrice + _auctionsDeatiels[auctionID].PriceJump;
            }

            return null;
        }

        public double? OfferLastChance(Guid auctionID, string agentName, double offerPrice)
        {
            return null;
        }
    }
}
