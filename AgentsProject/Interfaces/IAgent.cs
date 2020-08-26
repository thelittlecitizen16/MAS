using System;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Interfaces
{
    public interface IAgent
    {
         string Name { get; }
        bool EnterAuction(string productName, double startPrice, double priceJump);
        Tuple<double, IAgent> FirstOffer();
        Tuple<double, IAgent> NewOffer(string agentName, double offerPrice);
        Tuple<double, IAgent> OfferLastChance(string agentName, double offerPrice);
    }
}
