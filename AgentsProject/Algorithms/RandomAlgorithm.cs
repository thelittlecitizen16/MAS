using AgentsProject.Interfaces;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject.Algorithms
{
    public class RandomAlgorithm 
    {
        private Random _rand;
        public RandomAlgorithm()
        {
            _rand = new Random();
        }
        public bool EnterAuction(Guid auctionID, AuctionDeatiels auctionDeatiels)
        {
            int num =_rand.Next(1, 3);
            if (num==1)
            {
                return true;
            }

            return false;
        }

        public double? FirstOffer(Guid auctionID)
        {
            throw new NotImplementedException();
        }

        public double? NewOffer(Guid auctionID, string agentName, double offerPrice)
        {
            throw new NotImplementedException();
        }

        public double? OfferLastChance(Guid auctionID, string agentName, double offerPrice)
        {
            throw new NotImplementedException();
        }
    }
}
