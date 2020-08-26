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
        private bool _timeEndLastChance;
        private AuctionDeatiels _auctionDeatiels;

        public RunAuction(ManageAuction manageAuction)
        {
            _manageAuction = manageAuction;
            _manageAgents = new ManageAgents();
            _timeEnd = false;
            _timeEndLastChance = false;
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

            }

            Task.WaitAll(tasks.ToArray());
        }

        public void Run()
        {

            SendAboutNewAuction();
            _manageAuction.StartAuction();

            List<Tuple<double?, IAgent>> allResults = _manageAuction.SendAgentIfWantToAddFirstOffer();

            Print(allResults);
            _manageAuction.AddFirstOffer(allResults);
            _manageAuction.CheckOffer(allResults);

            List<Tuple<double?, IAgent>> resultsLastChance = RunAuctionAndLastChance();


            while (resultsLastChance.Count > 0)
            {
                resultsLastChance = RunAuctionAndLastChance();
            }

            _manageAuction.EndAuction();
        }
        private void RunTimeAuction()
        {
            var aTimer = new Timer(1000);
            aTimer.Elapsed += OnTimedEventOfAuction;

            while (!_timeEnd)
            {
                List<Tuple<double?, IAgent>> allResults = _manageAuction.SendAgentIfWantToAddNewOffer();
                Print(allResults);

                aTimer.Enabled = true;

                if (allResults.Where(r => r.Item1.HasValue).Any())
                {
                    aTimer.Enabled = false;
                    _manageAuction.CheckOffer(allResults);
                }
            }

            _timeEnd = false;
        }
        private List<Tuple<double?, IAgent>> AuctionLastChance()
        {
            _manageAuction.SendLastChance();
            var aTimer = new Timer(1000);
            aTimer.Elapsed += OnTimedEventOfLastChance;

            List<Tuple<double?, IAgent>> allResults;

            while (!_timeEndLastChance)
            {
                allResults = _manageAuction.SendAgentIfWantToAddLastOffer();

                aTimer.Enabled = true;

                if (allResults.Where(r => r.Item1.HasValue).Any())
                {
                    _timeEndLastChance = false;
                     Print(allResults);
                    _manageAuction.CheckOffer(allResults);

                    return allResults;
                }
            }

            allResults = new List<Tuple<double?, IAgent>>();
            _timeEndLastChance = false;

            return allResults;
        }

        private List<Tuple<double?, IAgent>> RunAuctionAndLastChance()
        {
            RunTimeAuction();
            return AuctionLastChance();
        }
        private void Print(List<Tuple<double?, IAgent>> results)
        {

            foreach (var result in results)
            {
                if (result!= null)
                {
                    if (result.Item1.HasValue)
                    {
                        if (_manageAuction.IsJumpOk(result.Item1.Value))
                        {
                            Console.WriteLine($"the agent {result.Item2.Name} add offer with the price {result.Item1.Value}");
                        }
                    }     
                }
            }
        }
        private void OnTimedEventOfAuction(Object source, System.Timers.ElapsedEventArgs e)
        {
            _timeEnd = true;
        }
        private void OnTimedEventOfLastChance(Object source, System.Timers.ElapsedEventArgs e)
        {
            _timeEndLastChance = true;
        }

    }
}
