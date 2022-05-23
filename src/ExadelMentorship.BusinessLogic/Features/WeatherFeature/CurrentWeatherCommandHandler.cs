using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public class CurrentWeatherCommandHandler : ICommandHandler<CurrentWeatherCommand>
    {
        IRWOperation _rwOperation;
        public CurrentWeatherCommandHandler(IRWOperation rwOperation)
        {
            _rwOperation = rwOperation;
        }

        public async Task Handle(ICurrentWeatherCommand currentWeather)
        {
            _rwOperation.WriteLine("Please enter the city Name:");
            var city = this.GetCityFromInput();

            WeatherHelper.ValidateCityName(city);
            city.Temperature = await currentWeather.GetTemperatureByCityName(city.Name);
            city.Comment = WeatherHelper.GetCommentByTemperature(city.Temperature);
            _rwOperation.WriteLine($"In {city.Name} temperature is: {city.Temperature}, {city.Comment}");
        }

        private City GetCityFromInput()
        {
            var inputedLine = _rwOperation.ReadLine();
            return new City
            {
                Name = inputedLine
            };
        }
    }
}
