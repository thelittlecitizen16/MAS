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

            runAuction = auctionfactory.CreateAuction(manageAgents, "one", DateTime.Now.AddSeconds(10), TimeSpan.FromSeconds(10), manageProducts.AllProducts.First(), 200, 100);
            runAuction2 = auctionfactory.CreateAuction(manageAgents, "two", DateTime.Now.AddSeconds(10), TimeSpan.FromSeconds(10), manageProducts.AllProducts.First(), 200, 100);
        }

        public void Try()
        {
            Task.Delay(new TimeSpan(0, 0, 10)).ContinueWith(o => { runAuction.Run(); });
            Task.Delay(new TimeSpan(0, 0, 10)).ContinueWith(o => { runAuction2.Run(); });
        }

    }
}
