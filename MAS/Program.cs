using MAS.NewFolder;
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
            Auction auction = new Auction("a", DateTime.Now, TimeSpan.FromSeconds(10), manageProducts.AllProducts.First(), 200, 100);
            ManageAuction manageAuction = new ManageAuction(auction);
            RunAuction runAuction = new RunAuction(manageAuction);
            runAuction.Run();
            Console.ReadLine();
        }
       
    }
}
