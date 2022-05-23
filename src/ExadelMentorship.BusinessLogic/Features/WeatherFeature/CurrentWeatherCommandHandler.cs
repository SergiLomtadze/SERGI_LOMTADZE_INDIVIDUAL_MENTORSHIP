using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public class CurrentWeatherCommandHandler : ICommandHandler<CurrentWeatherCommand>
    {
        private readonly IRWOperation _rwOperation;
        private readonly ICurrentWeatherService _currentWeatherService;
        public CurrentWeatherCommandHandler(IRWOperation rwOperation, ICurrentWeatherService currentWeatherService)
        {
            _rwOperation = rwOperation;
            _currentWeatherService = currentWeatherService;
        }

        public async Task Handle(CurrentWeatherCommand currentWeatherCommand)
        {
            _rwOperation.WriteLine("Please enter the city Name:");
            var city = this.GetCityFromInput();

            WeatherHelper.ValidateCityName(city);
            city.Temperature = await _currentWeatherService.GetTemperatureByCityName(city.Name);
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
