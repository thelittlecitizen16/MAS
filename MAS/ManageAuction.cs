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
        private event Func<string, double, Tuple<double?, IAgent>> NewOffer;
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
            Console.WriteLine($" the product is {Auction.Product.Name} the start price is {Auction.StartPrice}");
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

        public List<Tuple<double, IAgent>> SendAgentIfWantToAddFirstOffer()
        {
            List<Task> tasks = new List<Task>(); ;
            List<Tuple<double, IAgent>> allResults = new List<Tuple<double, IAgent>>();
            var b = FirstOffer.GetInvocationList();
            foreach (var c in b)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    var outputMsg = c;
                    Tuple<double, IAgent> tuple = (Tuple<double, IAgent>)outputMsg?.DynamicInvoke();
                    allResults.Add(tuple);
                    //allResults.Add(outputMsg?.DynamicInvoke());
                }));
            }
            Task.WaitAll(tasks.ToArray());

            return allResults;
            //FirstOffer.GetInvocationList().ToList().ForEach(x =>
            //Console.WriteLine(x.Method.ReturnType));
            //Array.ForEach(FirstOffer.GetInvocationList(), x =>
            //{

            //    Console.WriteLine(x.Method.ReturnType);
            //});
        }
        public List<Tuple<double?, IAgent>> SendAgentIfWantToAddNewOffer()
        {
            List<Task> tasks = new List<Task>(); 

            List<Tuple<double?, IAgent>> allResults = new List<Tuple<double?, IAgent>>();

            Thread.Sleep(1000);

            var b = NewOffer.GetInvocationList();


            foreach (var c in b)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    // var outputMsg = NewOffer.GetInvocationList()[i];
                    var name = _lastAgentOffer.Name;
                    var price = _lastOfferPrice;
                    object args = new Object[] { name, price };
                Tuple<double?, IAgent> tuple = (Tuple<double?, IAgent>)c?.DynamicInvoke(name, price);
                    allResults.Add(tuple);
                }));
            }
            Task.WaitAll(tasks.ToArray());

            return allResults;
        }

        public void CheckOfferFirst(List<Tuple<double, IAgent>> allResults)
        {
            _lastAgentOffer = allResults.First().Item2;
            _lastOfferPrice = allResults.First().Item1;

            foreach (var result in allResults)
            {
                if (_lastOfferPrice < result.Item1)
                {
                    _lastOfferPrice = result.Item1;
                    _lastAgentOffer = result.Item2;
                }
            }
        }
        public void CheckOffer(List<Tuple<double?, IAgent>> allResults)
        {
            
            foreach (var result in allResults)
            {
                if (result.Item1.HasValue)
                {
                    if (_lastOfferPrice < result.Item1)
                    {
                        _lastOfferPrice = result.Item1.Value;
                        _lastAgentOffer = result.Item2;
                    }
                }
                
            }
        }
    }
}
