using AgentsProject.Interfaces;
using Common;
using MAS.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace MAS
{
    public class RunAuction
    {
        private ManageAuction _manageAuction;
        private ManageAgents _manageAgents;
        private bool _timeEnd;
        private AuctionDeatiels _auctionDeatiels;

        public RunAuction(ManageAuction manageAuction)
        {
            _manageAuction = manageAuction;
            _manageAgents = new ManageAgents();
            _timeEnd = false;
            _auctionDeatiels = new AuctionDeatiels(_manageAuction.Auction.Name, _manageAuction.Auction.StartPrice, _manageAuction.Auction.PriceJump);
        }
        public void SendAboutNewAuction()
        {
            List<Task> tasks = new List<Task>(); ;

            foreach (var agent in _manageAgents.AllAgents)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    if (agent.EnterAuction(_manageAuction.Auction.ID, _auctionDeatiels))
                    {
                        _manageAuction.Subscribe(agent);
                    }
                }));

                Task.WaitAll(tasks.ToArray());
            }
        }

        public void Run()
        {

            SendAboutNewAuction();
            _manageAuction.StartAuction();

            List<Tuple<double?, IAgent>> allResults = _manageAuction.SendAgentIfWantToAddFirstOffer();

            Print(allResults);
            _manageAuction.AddFirstOffer(allResults);
            _manageAuction.CheckOffer(allResults);

            RunTimeAuction();

            Console.WriteLine("end");

        }
        private void RunTimeAuction()
        {
            var aTimer = new Timer(1000);
            aTimer.Elapsed += OnTimedEvent;

            while (!_timeEnd)
            {
                List<Tuple<double?, IAgent>> allResultss = _manageAuction.SendAgentIfWantToAddNewOffer();
                Print(allResultss);

                aTimer.Enabled = true;

                if (allResultss.Where(r => r.Item1 > 0).Any())
                {
                    aTimer.Enabled = false;
                    _manageAuction.CheckOffer(allResultss);
                }
            }
        }

        private void Print(List<Tuple<double, IAgent>> results)
        {
            foreach (var result in results)
            {
                Console.WriteLine($"the agent {result.Item2.Name} add offer with the price {result.Item1}");
            }
        }
        private void Print(List<Tuple<double?, IAgent>> results)
        {

            foreach (var result in results)
            {
                if (result!= null)
                {
                    if (result.Item1.HasValue)
                    {
                        Console.WriteLine($"the agent {result.Item2.Name} add offer with the price {result.Item1}");
                    }     
                }
            }
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            _timeEnd = true;
        }

    }
}
