using System;
using System.Collections.Generic;
using System.Text;

namespace AgentsProject
{
    public class AuctionDeatiels
    {
        public string ProductName { get;  }
        public double StartPrice { get; }
        public double PriceJump { get; }

        public AuctionDeatiels(string productName, double startPrice, double priceJump)
        {
            ProductName = productName;
            StartPrice = startPrice;
            PriceJump = priceJump;
        }
    }
}
