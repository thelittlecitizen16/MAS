using System;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Interfaces
{
    public interface IAgent
    {
        bool EnterAuction(string productName, double startPrice, double priceJump, List<IAgent> allAgentsInAuction);
        double NewOffer(string agentName, double offerPrice);
        double OfferLastChance(string agentName, double offerPrice);
    }
}
