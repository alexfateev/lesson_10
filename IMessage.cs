using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_10
{
    internal interface IMessage
    {
        void Message(string text);

        void Message(string[] arr);
    }
}
