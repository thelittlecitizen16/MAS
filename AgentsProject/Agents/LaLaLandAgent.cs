using AgentsProject.Algorithms;
using AgentsProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Agents
{
    public class LaLaLandAgent : IAgent
    {
        public string Name { get; private set; }
        private BasicAlgorithm _algorithm;
        public LaLaLandAgent()
        {
            _algorithm = new BasicAlgorithm();
            Name = "La La Land";
        }
        public bool EnterAuction(string productName, double startPrice, double priceJump, List<IAgent> allAgentsInAuction)
        {
            return _algorithm.EnterAuction(productName, startPrice, priceJump, allAgentsInAuction);
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
