using ExadelMentorship.BusinessLogic.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic
{
    public class ConsoleOperation : IRWOperation
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
