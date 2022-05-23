using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic
{
    public class MainJob
    {
        readonly IRWOperation _rwOperation;
        readonly CommandInvoker _commandInvoker;
        readonly IServiceProvider  _serviceProvider;
        public MainJob(IRWOperation rwOperation, CommandInvoker commandInvoker, IServiceProvider serviceProvider)
        {
            _rwOperation = rwOperation;
            _commandInvoker = commandInvoker;
            _serviceProvider = serviceProvider;
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
            if (commnad == 1)
            {
                return new CurrentWeatherCommand();
            }
            throw new NotImplementedException();
        }

    }
}
