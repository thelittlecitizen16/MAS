using AgentsProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Algorithms
{
    public class BasicAlgorithm : IAlgorithm
    {
        private string _productName;
        private double _startPrice;
        private double _priceJump;
        private List<IAgent> _allAgentsInAuction;

        public bool EnterAuction(string productName, double startPrice, double priceJump, List<IAgent> allAgentsInAuction)
        {
            _productName = productName;
            _startPrice = startPrice;
            _priceJump = priceJump;
            _allAgentsInAuction = allAgentsInAuction;
            return true;
        }

        public double NewOffer(string agentName, double offerPrice)
        {
            if (offerPrice < 500)
            {
                return offerPrice + _priceJump;
            }

            return 0;
        }

        public double OfferLastChance(string agentName, double offerPrice)
        {
            return 0;
        }
    }
}
