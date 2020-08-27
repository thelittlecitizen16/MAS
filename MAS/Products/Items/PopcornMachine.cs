using MAS.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.Products.Items
{
    public class PopcornMachine : IProduct
    {
        public string Name => "popcorn machine";

        public string Description => "new and tasty";
    }
}
