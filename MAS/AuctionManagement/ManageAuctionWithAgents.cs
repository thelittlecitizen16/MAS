using AgentsProject.Interfaces;
using MAS.Interfaces;
using MAS.MasDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS.AuctionManagement
{
    public class ManageAuctionWithAgents
    {
        public Auction Auction;
        public IAgent LastAgentOffer;
        public double LastOfferPrice;
        private event Func<Guid, Tuple<double?, IAgent>> FirstOffer;
        private event Func<Guid, string, double, Tuple<double?, IAgent>> NewOffer;
        private event Func<Guid, string, double, Tuple<double?, IAgent>> LastOffer;
        private event Action<Guid> EndAuctionToAgent;

        public ManageAuctionWithAgents(Auction auction)
        {
            Auction = auction;
            LastOfferPrice = 0;
        }
        public void Subscribe(IAgent agent)
        {
            SendIfWantToAddFirstOffer(agent);
            SendIfWantToAddNewOffer(agent);
            LastChance(agent);
            EventEndAuction(agent);
        }

        private void SendIfWantToAddFirstOffer(IAgent agent)
        {
            FirstOffer += agent.FirstOffer;
        }

        private void SendIfWantToAddNewOffer(IAgent agent)
        {
            NewOffer += agent.NewOffer;
        }

        private void LastChance(IAgent agent)
        {
            LastOffer += agent.OfferLastChance;
        }

        private void EventEndAuction(IAgent agent)
        {
            EndAuctionToAgent += agent.EndAuction;
        }

        public void RemoveAllAgentsFromEvent(List<IAgent> agents)
        {
            foreach (var agent in agents)
            {
                FirstOffer -= agent.FirstOffer;
                NewOffer -= agent.NewOffer;
                LastOffer -= agent.OfferLastChance;
                EndAuctionToAgent -= agent.EndAuction;
            }
        }

        public List<Tuple<double?, IAgent>> SendAgentIfWantToAddFirstOffer()
        {
            var parameter = new object[] { Auction.ID };
            return SendAgentAboutOffer(FirstOffer.GetInvocationList(), parameter);
        }

        public List<Tuple<double?, IAgent>> SendAgentIfWantToAddNewOffer()
        {
            var parameters = new object[] { Auction.ID, LastAgentOffer.Name, LastOfferPrice };
            return SendAgentAboutOffer(NewOffer.GetInvocationList(), parameters);
        }

        public List<Tuple<double?, IAgent>> SendAgentIfWantToAddLastOffer()
        {
            var parameters = new object[] { Auction.ID, LastAgentOffer.Name, LastOfferPrice };
            return SendAgentAboutOffer(LastOffer.GetInvocationList(), parameters);
        }

        public List<Tuple<double?, IAgent>> SendAgentAboutEndAuction()
        {
            var parameters = new object[] { Auction.ID };
            return SendAgentAboutOffer(EndAuctionToAgent.GetInvocationList(), parameters);
        }

        private List<Tuple<double?, IAgent>> SendAgentAboutOffer(Delegate[] allAgentsInAuctions, params object[] paramaters)
        {
            List<Task> tasks = new List<Task>();

            List<Tuple<double?, IAgent>> allResults = new List<Tuple<double?, IAgent>>();

            foreach (var agent in allAgentsInAuctions)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    Tuple<double?, IAgent> tuple = (Tuple<double?, IAgent>)agent?.DynamicInvoke(paramaters);
                    allResults.Add(tuple);
                }));
            }

            Task.WaitAll(tasks.ToArray());

            return allResults;
        }

    }
}
