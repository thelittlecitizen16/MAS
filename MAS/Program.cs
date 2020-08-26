using MAS.MasDB;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MAS
{
    class Program
    {
        static void Main(string[] args)
        {
            ManageProducts manageProducts = new ManageProducts();
            //Auction auction = new Auction("a", DateTime.Now, TimeSpan.FromSeconds(10), manageProducts.AllProducts.First(), 200, 100);
            //ManageAuction manageAuction = new ManageAuction(auction);
            ManageAgents manageAgents = new ManageAgents();
            //RunAuction runAuction = new RunAuction(manageAuction, manageAgents);
            //runAuction.Run();
            ManageFewAuctions manageFewAuctions = new ManageFewAuctions(manageAgents,manageProducts);
            manageFewAuctions.Try();
            Console.ReadLine();
        }
       
    }
}
