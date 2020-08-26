using AgentsProject.Interfaces;
using MAS.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public class Auction
    {
        public Guid ID { get; private set; }
        public string Name { get; private set; }
        public DateTime StartTime { get; private set; }
        public TimeSpan WaitWithoutOffer { get; private set; }
        public IProduct Product { get; private set; }
        public double StartPrice { get; private set; }
        public double PriceJump { get; private set; }


        public Auction(string name, DateTime startTime, TimeSpan waitWithoutOffer, IProduct product, double startPrice, double priceJump)
        {
            ID = Guid.NewGuid();
            Name = name;
            StartPrice = startPrice;
            WaitWithoutOffer = waitWithoutOffer;
            Product = product;
            StartPrice = startPrice;
            PriceJump = priceJump;
        }

     

       


    }
}
