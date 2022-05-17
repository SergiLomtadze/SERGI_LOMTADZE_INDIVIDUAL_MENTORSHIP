using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic
{
    public class ConsoleJob
    {
        IRWOperation _consoleOperation;
        public ConsoleJob(IRWOperation consoleOperation)
        {
            _consoleOperation = consoleOperation;
        }

        public City GetCityFromConsole()
        {
            var inputedLine = _consoleOperation.ReadLine();
            return new City
            {
                Name = inputedLine
            };
        }

        public async Task DoJob(Weather weather)
        {
         
            while (true)
            {
                _consoleOperation.WriteLine("Please enter the city Name:");
                var city = this.GetCityFromConsole();

                try
                {
                    weather.ValidateCityName(city);
                    city.Temperature = await weather.GetTemperatureByCityName(city.Name);
                    city.Comment = WeatherHelper.GetCommentByTemperature(city.Temperature);
                    _consoleOperation.WriteLine($"In {city.Name} temperature is: {city.Temperature}, {city.Comment}");
                }

                catch (NotFoundException exception)
                {
                    _consoleOperation.WriteLine(exception.Message);
                }

            }
        }
    }
}
