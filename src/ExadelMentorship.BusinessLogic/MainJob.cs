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
    public class MainJob
    {
        IRWOperation _rwOperation;
        public MainJob(IRWOperation rwOperation)
        {
            _rwOperation = rwOperation;
        }

        public City GetCityFromInput()
        {
            var inputedLine = _rwOperation.ReadLine();
            return new City
            {
                Name = inputedLine
            };
        }

        public async Task Execute(Weather weather)
        {
            while (true)
            {
                _rwOperation.WriteLine("Please enter the city Name:");
                var city = this.GetCityFromInput();

                try
                {
                    weather.ValidateCityName(city);
                    city.Temperature = await weather.GetTemperatureByCityName(city.Name);
                    city.Comment = WeatherHelper.GetCommentByTemperature(city.Temperature);
                    _rwOperation.WriteLine($"In {city.Name} temperature is: {city.Temperature}, {city.Comment}");
                }

                catch (NotFoundException exception)
                {
                    _rwOperation.WriteLine(exception.Message);
                }

            }
        }
    }
}
