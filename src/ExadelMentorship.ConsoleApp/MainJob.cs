using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.MaxWeather;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.ConsoleApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        await ParseCommand(command);
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

        private async Task ParseCommand (int commnad)
        {
            if (commnad == 1)
            {
                _rwOperation.WriteLine("Please enter the city Name:");
                var result = new CurrentWeatherCommand
                {
                    CityName = _rwOperation.ReadLine()
                };
                var response = await _commandInvoker.Invoke(result);
                _rwOperation.WriteLine(Texts.CurrentWeatherCommandResponse, response.Name, response.Temperature, response.Comment);
            }
            if (commnad == 2)
            {
                FutureWeatherCommand futureWeatherCommand = new FutureWeatherCommand();
                
                _rwOperation.WriteLine("Please enter the city Name:");
                futureWeatherCommand.CityName = _rwOperation.ReadLine();

                _rwOperation.WriteLine("Please enter interested days quantity:");
                futureWeatherCommand.DayQuantity = _rwOperation.ReadLine();

                var responce = await _commandInvoker.Invoke(futureWeatherCommand);
                foreach (var city in responce)
                {
                    _rwOperation.WriteLine($"Day {city.Date.ToString("dd/MM/yyyy")}: {city.Temperature}. {city.Comment}");
                }

            }
            if (commnad == 3)
            {
                _rwOperation.WriteLine("Please enter the cities:");
                var result = new MaxWeatherCommand
                {
                    Cities = _rwOperation.ReadLine(),
                };
                var response = await _commandInvoker.Invoke(result);
                if (response.SuccessfulRequests > 0)
                {
                    _rwOperation.WriteLine(Texts.SuccessfulRequest,
                        response.MaxTempCityInfo.Temperature,
                        response.MaxTempCityInfo.Name,
                        response.SuccessfulRequests,
                        response.FailedRequests,
                        response.CancelledRequests);

                    if (response.Statistic)
                    {
                        DebugInfoProvider(response.MaxTempCityInfoList);
                    }
                }
                else
                {
                    _rwOperation.WriteLine(Texts.NoSuccessful, response.FailedRequests, response.CancelledRequests);
                }
            }
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

        private void DebugInfoProvider(IEnumerable<MaxTempCityInfo> maxTempCityInfoList)
        {
            var lits = maxTempCityInfoList.ToList();
            foreach (var item in maxTempCityInfoList)
            {
                if (item.Name != null)
                {
                    _rwOperation.WriteLine(Texts.DebugInfo, item.Name, item.Temperature, item.DurationTime);
                }
                if (item.ErrorMessage != null)
                {
                    _rwOperation.WriteLine(item.ErrorMessage);
                }
            }

        }

    }

}
