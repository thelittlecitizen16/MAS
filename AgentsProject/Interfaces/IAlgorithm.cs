using System;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Interfaces
{
    public interface IAlgorithm
    {
        bool EnterAuction(string productName, double startPrice, double priceJump);
        double FirstOffer();
        double NewOffer(string agentName, double offerPrice);
        double OfferLastChance(string agentName, double offerPrice);
    }
}
