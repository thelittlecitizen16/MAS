using MAS.Products.Interfaces;
using MAS.Products.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.NewFolder
{
    public class ManageProducts
    {
        public List<IProduct> AllProducts;

        public ManageProducts()
        {
            AllProducts = new List<IProduct>();
            AddAllProducts();
        }

        private void AddAllProducts()
        {
            AllProducts.Add(new Office1());
            AllProducts.Add(new Office2());
        }
    }
}
