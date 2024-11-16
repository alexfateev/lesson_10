using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace lesson_10
{
    internal class CombineMessage : IMessage
    {
        private IMessage _consoleMessage;
        private IMessage _fileMessage;

        public CombineMessage(IMessage consoleMessage, IMessage fileMessage)
        {
            _consoleMessage = consoleMessage;
            _fileMessage = fileMessage;
        }
        public void Message(string text)
        {
            _consoleMessage.Message(text);
            _fileMessage.Message(text);
        }

        public void Message(string[] arr)
        {
            _consoleMessage.Message(arr);
        }
    }
}
