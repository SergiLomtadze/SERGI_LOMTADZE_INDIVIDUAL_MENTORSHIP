using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic
{
    public class ConsoleOperation : IRWOperation, ICommand
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public async Task Execute()
        {
            Environment.Exit(0);
        }
    }
}
