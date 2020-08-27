using System;
using System.Collections.Generic;
using System.Text;

namespace MAS.Interfaces
{
    public interface ISystem
    {
        string ReadString();
        void Write(string message, ConsoleColor color);
    }
}
