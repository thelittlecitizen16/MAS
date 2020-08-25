using AgentsProject.Algorithms;
using AgentsProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Agents
{
    public class BamasAgent : IAgent
    {
        public string Name { get; private set; }
        private BasicAlgorithm _algorithm;
        public BamasAgent()
        {
            _algorithm = new BasicAlgorithm();
            Name = "Bamas Family";
        }
        public bool EnterAuction(string productName, double startPrice, double priceJump)
        {
            return _algorithm.EnterAuction(productName, startPrice, priceJump);
        }

        public double NewOffer(string agentName, double offerPrice)
        {
            return _algorithm.NewOffer(agentName, offerPrice);
        }

        public double OfferLastChance(string agentName, double offerPrice)
        {
            return _algorithm.OfferLastChance(agentName, offerPrice);
        }
    }
}
