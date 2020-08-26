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
        public bool EnterAuction(string productName, double startPrice, double priceJump)
        {
            return _algorithm.EnterAuction(productName, startPrice, priceJump);
        }

        public Tuple<double, IAgent> FirstOffer()
        {
            return new Tuple<double, IAgent>(_algorithm.FirstOffer(), this);
        }

        public Tuple<double?, IAgent> NewOffer(string agentName, double offerPrice)
        {
            return new Tuple<double?, IAgent>(_algorithm.NewOffer(agentName, offerPrice), this);
        }

        public Tuple<double, IAgent> OfferLastChance(string agentName, double offerPrice)
        {
            return new Tuple<double, IAgent>(_algorithm.OfferLastChance(agentName, offerPrice), this);
        }
    }
}
