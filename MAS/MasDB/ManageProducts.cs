using MAS.Products.Interfaces;
using MAS.Products.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.MasDB
{
    public class ManageProducts
    {
        public List<IProduct> AllProducts;

        public ManageProducts()
        {
            AllProducts = new List<IProduct>();
            AddAllProducts();
        }
        public void RemoveProduct(IProduct product)
        {
            AllProducts.Remove(product);
        }
        public void AddAllProducts()
        {
            AllProducts.Add(new Office1());
            AllProducts.Add(new Office2());
        }
    }
}
