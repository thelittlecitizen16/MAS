using AgentsProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.Interfaces
{
    public interface IManageAuctionWithAgents
    {
        IAuction Auction { get; }
        IAgent LastAgentOffer { get; set; }
        double LastOfferPrice { get; set; }

        void Subscribe(IAgent agent);

        void RemoveAllAgentsFromEvent(List<IAgent> agents);

        List<Tuple<double?, IAgent>> SendAgentIfWantToAddFirstOffer();

        List<Tuple<double?, IAgent>> SendAgentIfWantToAddNewOffer();

        List<Tuple<double?, IAgent>> SendAgentIfWantToAddLastOffer();

        List<Tuple<double?, IAgent>> SendAgentAboutEndAuction();
    }
}
