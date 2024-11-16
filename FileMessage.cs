using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_10
{
    internal class FileMessage : IMessage
    {
        private string path = "log.txt";
        public void Message(string text)
        {
            using StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine($"Сообщение: {text}");
        }

        public void Message(string[] arr)
        {
            return;
        }
    }
}
