using MAS.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.Interfaces
{
    public interface IAuction
    {
         Guid ID { get; }
         string Name { get; }
         DateTime StartTime { get; }
         TimeSpan WaitWithoutOffer { get; }
         IProduct Product { get; }
         double StartPrice { get; }
         double PriceJump { get;}
    }
}
