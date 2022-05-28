using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic
{
    public class MainJob2
    {
        readonly IRWOperation _rwOperation;
        readonly CommandInvoker2 _commandInvoker;
        public MainJob2(IRWOperation rwOperation, CommandInvoker2 commandInvoker)
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

            if (result is IEnumerable<City>)
            {
                foreach (var city in result.cityList)
                {
                    _rwOperation.WriteLine($"Day {city.Date.ToString("dd/MM/yyyy")}: {city.Temperature}. {city.Comment}");
                }
              
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
                FutureWeatherCommand futureWeatherCommand = new FutureWeatherCommand();
                
                _rwOperation.WriteLine("Please enter the city Name:");
                futureWeatherCommand.CityName = _rwOperation.ReadLine();

                _rwOperation.WriteLine("Please enter interested days quantity:");
                futureWeatherCommand.DayQuantity = _rwOperation.ReadLine();
                
                return futureWeatherCommand;
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
