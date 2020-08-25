using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.Products.Interfaces
{
    public interface IOffice : IProduct
    {
        int NumberOfRooms { get; }
    }
}
