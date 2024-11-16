using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_10
{
    internal class ConsoleMessage : IMessage
    {
        public void Message(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{text}");
            Console.ResetColor();
        }

        public void Message(string[] arr)
        {
            foreach(string str in arr)
            {
                Console.WriteLine(str);
            }
        }
    }
}
