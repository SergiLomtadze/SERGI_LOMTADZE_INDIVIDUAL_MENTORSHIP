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
        ICurrentWeather _currentWeather;
        public MainJob(IRWOperation rwOperation, ICurrentWeather currentWeather)
        {
            _rwOperation = rwOperation;
            _currentWeather = currentWeather;
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
            try
            {
                await this.CurrentWeatherExecutor();
            }

            catch (NotFoundException exception)
            {
                _rwOperation.WriteLine(exception.Message);
            }

            _rwOperation.ReadLine();
        }

        public async Task CurrentWeatherExecutor()
        {
            _rwOperation.WriteLine("Please enter the city Name:");
            var city = this.GetCityFromInput();

            WeatherHelper.ValidateCityName(city);
            city.Temperature = await _currentWeather.GetTemperatureByCityName(city.Name);
            city.Comment = WeatherHelper.GetCommentByTemperature(city.Temperature);
            _rwOperation.WriteLine($"In {city.Name} temperature is: {city.Temperature}, {city.Comment}");
        }
    }
}
