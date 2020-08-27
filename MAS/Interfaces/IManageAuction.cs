using AgentsProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.Interfaces
{
    public interface IManageAuction
    {
        IManageAuctionWithAgents ManageAuctionAgents { get; }

        bool SendAboutNewAuction();

        void StartAuction();

        void EndAuction();

        public void SendLastChance();

        void CheckOffer(List<Tuple<double?, IAgent>> allResults);

        bool IsJumpOk(double price);
       
    }
}
