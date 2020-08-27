using MAS.AuctionManagement;
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

        private Auctionfactory auctionfactory;


        private RunAuction runAuction;
        private RunAuction runAuction2;

        public ManageFewAuctions(ManageAgents manageAgents, ManageProducts manageProducts)
        {
            auctionfactory = new Auctionfactory();

            runAuction = auctionfactory.CreateAuction(manageAgents, "one", DateTime.Now.AddSeconds(10), TimeSpan.FromSeconds(1000), manageProducts.AllProducts.First(), 200, 100);
            runAuction2 = auctionfactory.CreateAuction(manageAgents, "two", DateTime.Now.AddSeconds(10), TimeSpan.FromSeconds(1000), manageProducts.AllProducts.First(), 200, 100);
        }

        public void Try()
        {

            Task.Delay(CreateTimeToWait(runAuction.ManageAuction.Auction.StartTime)).ContinueWith(o => { runAuction.Run(); });
            Task.Delay(CreateTimeToWait(runAuction2.ManageAuction.Auction.StartTime)).ContinueWith(o => { runAuction2.Run(); });
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
