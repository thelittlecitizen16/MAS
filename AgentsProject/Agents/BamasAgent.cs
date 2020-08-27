using AgentsProject.Algorithms;
using AgentsProject.Interfaces;
using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Agents
{
    public class BamasAgent : IAgent
    {
        public string Name { get; private set; }
        private RandomAlgorithm _algorithm;
        public double MoneyAccount { get; private set; }

        public ConcurrentDictionary<Guid, AuctionDeatiels> AuctionsDeatiels { get; private set; }

        public BamasAgent()
        {
            AuctionsDeatiels = new ConcurrentDictionary<Guid, AuctionDeatiels>();
            _algorithm = new RandomAlgorithm();
            Name = "Bamas Family";
            MoneyAccount = 1000;
        }
        public bool EnterAuction(Guid auctionID, AuctionDeatiels auctionDeatiels)
        {
            bool chice = _algorithm.EnterAuction(auctionID, auctionDeatiels); 

            if (chice)
            {
                AuctionsDeatiels.TryAdd(auctionID, auctionDeatiels);
            }

            return chice;
        }
        public Tuple<double?, IAgent> FirstOffer(Guid auctionID)
        {
            return  new Tuple<double?, IAgent>( _algorithm.FirstOffer(auctionID, AuctionsDeatiels[auctionID]) , this);
        }

        public Tuple<double?, IAgent> NewOffer(Guid auctionID,string agentName, double offerPrice)
        {
            return new Tuple<double?, IAgent>(_algorithm.NewOffer(auctionID, agentName, offerPrice, AuctionsDeatiels[auctionID]), this);
        }

        public Tuple<double?, IAgent> OfferLastChance(Guid auctionID,string agentName, double offerPrice)
        {
            return new Tuple<double?, IAgent>(_algorithm.OfferLastChance(auctionID,agentName, offerPrice, AuctionsDeatiels[auctionID]), this);
        }

        public void TakeMoneyWhenWin(double priceToPay)
        {
            MoneyAccount -= priceToPay;
        }
        public void EndAuction(Guid auctionID)
        {
            AuctionDeatiels auctionDeatiels;
            AuctionsDeatiels.TryRemove(auctionID, out auctionDeatiels);
        }
    }
}
