using MAS.AuctionManagement;
using MAS.Interfaces;
using MAS.MasDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS
{
    public class ManageFewAuctions
    {

        private Auctionfactory _auctionfactory;
        private ManageProducts _manageProducts;
        private ISystem _system;
        private ManageAgents _manageAgents;
        private Random rand;

        public ManageFewAuctions(ISystem system , ManageAgents manageAgents, ManageProducts manageProducts)
        {
            _auctionfactory = new Auctionfactory();
            _system = system;
            _manageAgents = manageAgents;
            _manageProducts = manageProducts;
            rand = new Random();
        }

        public void RunAllAuctionsForProducts()
        {
            List<RunAuction> allAuctions = CreateAuctionEachProduct();
            List<Task> allAuctionTasks = CreateTaskEachAuction(allAuctions);

            Task.WaitAll(allAuctionTasks.ToArray());
        }
        private List<RunAuction> CreateAuctionEachProduct()
        {
            List<RunAuction> allAuctions = new List<RunAuction>();
            foreach (var product in _manageProducts.AllProducts)
            {
                double price = rand.Next(50, 500);
                double jumpPrice = rand.Next(50, 200);
                int seconds = rand.Next(0, 30);

                allAuctions.Add(_auctionfactory.CreateAuction(_system, _manageProducts, _manageAgents, "WoW", DateTime.Now.AddSeconds(seconds), TimeSpan.FromSeconds(1000), product, price, jumpPrice));
            }

            return allAuctions;
        }
        private List<Task> CreateTaskEachAuction(List<RunAuction> allAuctions)
        {
            List<Task> allAuctionTasks = new List<Task>();

            foreach (var auction in allAuctions)
            {
                allAuctionTasks.Add(Task.Delay(CreateTimeToWait(auction.ManageAuction.ManageAuctionAgents.Auction.StartTime))
                .ContinueWith(o => { auction.Run(); }));
            }
            return allAuctionTasks;
        }

        private TimeSpan CreateTimeToWait(DateTime date)
        {
            if (date > DateTime.Now)
            {
                int seconds = (int)(date - DateTime.Now).TotalSeconds;
                return new TimeSpan(0, 0, seconds);
            }
            else
            {
                return new TimeSpan(0, 0, 0);
            }
        }
    }
}
