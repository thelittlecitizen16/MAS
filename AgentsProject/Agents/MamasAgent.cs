using AgentsProject.Algorithms;
using AgentsProject.Interfaces;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Agents
{
    public class MamasAgent : IAgent
    {
        public string Name { get; private set; }
        private BasicAlgorithm _algorithm;
        public MamasAgent()
        {
            _algorithm = new BasicAlgorithm();
            Name = "Mams Empire";
        }
        public bool EnterAuction(Guid auctionID, AuctionDeatiels auctionDeatiels)
        {
            return _algorithm.EnterAuction(auctionID, auctionDeatiels);
        }
        public Tuple<double?, IAgent> FirstOffer(Guid auctionID)
        {
            return new Tuple<double?, IAgent>(_algorithm.FirstOffer(auctionID), this);
        }

        public Tuple<double?, IAgent> NewOffer(Guid auctionID, string agentName, double offerPrice)
        {
            return new Tuple<double?, IAgent>(_algorithm.NewOffer(auctionID, agentName, offerPrice), this);
        }

        public Tuple<double?, IAgent> OfferLastChance(Guid auctionID, string agentName, double offerPrice)
        {
            return new Tuple<double?, IAgent>(600, this);
        }
    }
}
