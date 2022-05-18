using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Interfaces;
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
        IWeather _weather;
        public MainJob(IRWOperation rwOperation, IWeather weather)
        {
            _rwOperation = rwOperation;
            _weather = weather;
        }

        private City GetCityFromInput()
        {
            var inputedLine = _rwOperation.ReadLine();
            return new City
            {
                Name = inputedLine
            };
        }

        public async Task Execute()
        {
            _rwOperation.WriteLine("Please enter the city Name:");
            var city = this.GetCityFromInput();

            try
            {
                _weather.ValidateCityName(city);
                city.Temperature = await _weather.GetTemperatureByCityName(city.Name);
                city.Comment = WeatherHelper.GetCommentByTemperature(city.Temperature);
                _rwOperation.WriteLine($"In {city.Name} temperature is: {city.Temperature}, {city.Comment}");
            }

            catch (NotFoundException exception)
            {
                _rwOperation.WriteLine(exception.Message);
            }

            _rwOperation.ReadLine();
        }
    }
}
