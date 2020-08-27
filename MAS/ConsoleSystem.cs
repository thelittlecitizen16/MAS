using MAS.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAS
{
    public class ConsoleSystem : ISystem
    {
        public string ReadString()
        {
            return Console.ReadLine();
        }

        public void Write(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
        }
    }
}
