using AgentsProject.Interfaces;
using Common;
using MAS.Interfaces;
using MAS.MasDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace MAS.AuctionManagement
{
    public class RunAuction
    {
        public ManageAuction ManageAuction;
        private ManageAgents _manageAgents;
        private bool _timeEndFirstChance;
        private bool _timeEnd;
        private bool _timeEndLastChance;
        private ISystem _system;
        private ConsoleColor _color;

        public RunAuction(ManageAuction manageAuction, ManageAgents manageAgents, ISystem system, ConsoleColor color)
        {
            ManageAuction = manageAuction;
            _manageAgents = manageAgents;
            _system = system;
            _color = color;
            _timeEnd = false;
            _timeEndLastChance = false;
        }

        public void Run()
        {
            if (ManageAuction.SendAboutNewAuction())
            {
                ManageAuction.StartAuction();

                if (!RunFirstTimeAuction())
                {
                    _system.Write("no one add offer!", _color);
                }
            }
            else
            {
                _system.Write("No One Enter To The Auction", _color);
            }
        }
        private bool RunFirstTimeAuction()
        {
            var aTimer = new Timer(ManageAuction.ManageAuctionAgents.Auction.WaitWithoutOffer.TotalSeconds);
            aTimer.Elapsed += OnTimedEventOfFirstStartAuction;

            bool HaveOffer = false; ;

            while (!_timeEndFirstChance)
            {
                List<Tuple<double?, IAgent>> allResults = ManageAuction.ManageAuctionAgents.SendAgentIfWantToAddFirstOffer();
                allResults = allResults.Where(r => r != null).ToList();

                if (allResults.Where(r => r.Item1.HasValue).Any())
                {
                    aTimer.Enabled = false;
                    HaveOffer = true;

                    Print(allResults);

                    ManageAuction.CheckOffer(allResults);
                    List<Tuple<double?, IAgent>> resultsLastChance = RunAuctionAndLastChance();

                    while (resultsLastChance.Count > 0)
                    {
                        resultsLastChance = RunAuctionAndLastChance();
                    }

                    ManageAuction.EndAuction();

                    _timeEndFirstChance = true;
                }

                aTimer.Enabled = true;
            }

            return HaveOffer;
        }
        private void RunTimeAuction()
        {
            var aTimer = new Timer(ManageAuction.ManageAuctionAgents.Auction.WaitWithoutOffer.TotalSeconds);
            aTimer.Elapsed += OnTimedEventOfAuction;

            while (!_timeEnd)
            {
                List<Tuple<double?, IAgent>> allResults = ManageAuction.ManageAuctionAgents.SendAgentIfWantToAddNewOffer();
                Print(allResults);

                aTimer.Enabled = true;

                allResults = allResults.Where(r => r != null).ToList();
                if (allResults.Where(r => r.Item1.HasValue).Any())

                {
                    aTimer.Enabled = false;
                    ManageAuction.CheckOffer(allResults);
                }

            }

            _timeEnd = false;
        }
        private List<Tuple<double?, IAgent>> AuctionLastChance()
        {
            ManageAuction.SendLastChance();
            var aTimer = new Timer(ManageAuction.ManageAuctionAgents.Auction.WaitWithoutOffer.TotalSeconds);
            aTimer.Elapsed += OnTimedEventOfLastChance;

            List<Tuple<double?, IAgent>> allResults;

            while (!_timeEndLastChance)
            {
                allResults = ManageAuction.ManageAuctionAgents.SendAgentIfWantToAddLastOffer();

                aTimer.Enabled = true;
                if (allResults.Count > 0)
                {
                    if (allResults.Where(r => r.Item1.HasValue).Any())
                    {
                        _timeEndLastChance = false;
                        Print(allResults);
                        ManageAuction.CheckOffer(allResults);

                        return allResults;
                    }
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
                if (result != null)
                {
                    if (result.Item1.HasValue)
                    {
                        if (ManageAuction.IsJumpOk(result.Item1.Value))
                        {
                            _system.Write($"the agent {result.Item2.Name} add offer with the price {result.Item1.Value} in aucction {ManageAuction.ManageAuctionAgents.Auction.ID}", _color);
                        }
                    }
                }
            }
        }
        private void OnTimedEventOfFirstStartAuction(Object source, System.Timers.ElapsedEventArgs e)
        {
            _timeEndFirstChance = true;
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
