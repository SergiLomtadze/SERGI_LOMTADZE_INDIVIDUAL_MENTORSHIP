using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic
{
    public class MainJob
    {
        readonly IRWOperation _rwOperation;
        readonly CommandInvoker _commandInvoker;
        public MainJob(IRWOperation rwOperation, CommandInvoker commandInvoker)
        {
            _rwOperation = rwOperation;
            _commandInvoker = commandInvoker;
        }

        public Task Execute()
        {
            while (true)
            {
                var command = this.GetActionFromUser();
                _commandInvoker.Invoke(ParseCommand(command));
                _rwOperation.ReadLine();
            }
        }

        private int GetActionFromUser()
        {
            string inputedLine;
            do
            {
                Console.WriteLine("Please enter Number \n" +
                    "1 - Current weather \n" +
                    "2 - Future weather \n" +
                    "0 - Close");
                inputedLine = Console.ReadLine();
            } while (!(inputedLine.Equals("0") || inputedLine.Equals("1") || inputedLine.Equals("2")));
            return Convert.ToInt32(inputedLine);
        }

        private ICommand ParseCommand(int commnad)
        {
            throw new NotImplementedException();
        }

    }
}
