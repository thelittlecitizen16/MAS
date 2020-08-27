using MAS.MasDB;
using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using System.Threading.Tasks;

namespace MAS
{
    class Program
    {
        static void Main(string[] args)
        {
            ManageProducts manageProducts = new ManageProducts();
            ManageAgents manageAgents = new ManageAgents();
            ConsoleSystem consoleSystem = new ConsoleSystem();
           
            ManageFewAuctions manageFewAuctions = new ManageFewAuctions(consoleSystem, manageAgents, manageProducts);
            manageFewAuctions.RunAllAuctionsForProducts();
        }
    }
}
