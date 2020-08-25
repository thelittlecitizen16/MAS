using MAS.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.Products.Items
{
    public class Office1 : IOffice
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int NumberOfRooms { get; private set; }

        public Office1()
        {
            Name = "Office one";
            Description = "main building office number 1";
            NumberOfRooms = 3;
        }
    }
}
