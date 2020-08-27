using MAS.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.Products.Items
{
    public class Office2 : IOffice
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int NumberOfRooms { get; private set; }

        public Office2()
        {
            Name = "Office two";
            Description = "main building office number 2";
            NumberOfRooms = 2;
        }
    }
}
