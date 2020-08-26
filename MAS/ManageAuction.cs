using AgentsProject.Interfaces;
using MAS.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MAS
{
    public class ManageAuction
    {
        public Auction Auction;
        private IAgent _lastAgentOffer;
        private double _lastOfferPrice;
        private event Func<Tuple<double, IAgent>> FirstOffer;
        private event Func<string, double, Tuple<double, IAgent>> NewOffer;
        private event Func<string, double, Tuple<double, IAgent>> LastOffer;

        public ManageAuction(Auction auction)
        {
            Auction = auction;
        }
        public void Subscribe(IAgent agent)
        {
            SendIfWantToAddFirstOffer(agent);
            SendIfWantToAddNewOffer(agent);
            LastChance(agent);
        }

        public void StartAuction()
        {
            Console.WriteLine($"the auction {Auction.Name} start now-");
            Console.WriteLine($" the product is {Auction.Product} the start price is {Auction.StartPrice}");
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

        public void EndAuction()
        {
            Console.WriteLine($"the auction over-");
            Console.WriteLine($"{_lastAgentOffer.Name} is the winner and he buy in {_lastOfferPrice}");

        }

        public void SendIfWantToAddFirstOffer()
        {
            List<Tuple<double, IAgent>> allResults = new List<Tuple<double, IAgent>>();
            Thread.Sleep(1000);

            for (int i = 0; i <= FirstOffer.GetInvocationList().Length; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    var outputMsg = FirstOffer.GetInvocationList()[i];
                    Tuple<double, IAgent> tuple = (Tuple<double, IAgent>)outputMsg?.DynamicInvoke();
                    allResults.Add(tuple);
                    //allResults.Add(outputMsg?.DynamicInvoke());
                    Console.WriteLine(tuple.Item1.ToString(), tuple.Item2.Name); 
                });
            }
            //FirstOffer.GetInvocationList().ToList().ForEach(x =>
            //Console.WriteLine(x.Method.ReturnType));
            //Array.ForEach(FirstOffer.GetInvocationList(), x =>
            //{

            //    Console.WriteLine(x.Method.ReturnType);
            //});
        }
    }
}
