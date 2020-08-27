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

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
