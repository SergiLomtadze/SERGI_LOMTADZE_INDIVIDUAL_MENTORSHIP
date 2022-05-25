using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.MaxWeather;
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

        public async Task Execute()
        {
            bool condition = true;
            do
            {
                var command = this.GetActionFromUser();
                if (command == 0)
                {
                    condition = false;
                }
                else
                {
                     try
                     {
                        await _commandInvoker.Invoke(ParseCommand(command));
                        _rwOperation.ReadLine();
                     } 
                      catch (NotFoundException ex)
                     {
                        _rwOperation.WriteLine($"{ex.Message.ToString()} \n");
                     }
                     catch (FormatException ex)
                     {
                        _rwOperation.WriteLine($"{ex.Message} \n");
                     }
                }
            } while (condition);
        }

        private int GetActionFromUser()
        {
            string inputedLine;
            do
            {
                _rwOperation.WriteLine("Please enter Number \n" +
                    "1 - Current weather \n" +
                    "2 - Future weather \n" +
                    "3 - Max weather \n" +
                    "0 - Close");
                inputedLine = _rwOperation.ReadLine();
            } while (!(inputedLine.Equals("0") || inputedLine.Equals("1") || inputedLine.Equals("2") || inputedLine.Equals("3")));
            return Convert.ToInt32(inputedLine);
        }

        private ICommand ParseCommand(int commnad)
         {
            if (commnad == 1)
            {
                return new CurrentWeatherCommand();
            }
            if (commnad == 2)
            {
                return new FutureWeatherCommand();
            }
            if (commnad == 3)
            {
                return new MaxWeatherCommand();
            }
            throw new NotImplementedException();
        }

    }
}
