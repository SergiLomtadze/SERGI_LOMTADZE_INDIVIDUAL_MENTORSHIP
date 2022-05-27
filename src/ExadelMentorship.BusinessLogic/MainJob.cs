using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
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
                        var result = await _commandInvoker.Invoke(ParseCommand(command));
                        PrintResult(result);
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

        private void PrintResult(dynamic result)
        {
            if (result is CurrentWeatherCommandResponse)
            {
                _rwOperation.WriteLine(Texts.CurrentWeatherCommandResponse,result.Name, result.Temperature, result.Comment);
            }
        }
        private dynamic ParseCommand(int commnad)
         {
            if (commnad == 1)
            {
                _rwOperation.WriteLine("Please enter the city Name:");
                return new CurrentWeatherCommand
                {
                    CityName = _rwOperation.ReadLine()
                };
            }
            if (commnad == 2)
            {
                //return new FutureWeatherCommand();
            }
            if (commnad == 3)
            {
                //return new MaxWeatherCommand();
            }
            throw new NotImplementedException();
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

    }
}
