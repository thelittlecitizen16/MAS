using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class AuctionDeatiels
    {
        public string ProductName { get; private set; }
        public double StartPrice { get; private set; }
        public double PriceJump { get; private set; }

        public AuctionDeatiels(string productName, double startPrice, double priceJump)
        {
            ProductName = productName;
            StartPrice = startPrice;
            PriceJump = priceJump;
        }
    }
}
