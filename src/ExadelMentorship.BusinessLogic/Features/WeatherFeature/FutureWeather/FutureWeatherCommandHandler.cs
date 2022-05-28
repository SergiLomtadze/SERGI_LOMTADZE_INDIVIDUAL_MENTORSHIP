using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather
{
    public class FutureWeatherCommandHandler : ICommandHandler<FutureWeatherCommand>
    {
        IRWOperation _rwOperation;
        private readonly IWeatherApiService _weatherApiService;
        private readonly IConfiguration _configuration;
        public FutureWeatherCommandHandler(IRWOperation rwOperation, IWeatherApiService weatherApiService, IConfiguration configuration)
        {
            _rwOperation = rwOperation;
            _weatherApiService = weatherApiService;
            _configuration = configuration;
        }

        public async Task Handle(FutureWeatherCommand futureWeather)
            {
                _rwOperation.WriteLine("Please enter the city Name:");
                var inputedCity = this.GetCityFromInput();
                WeatherHelper.ValidateCityName(inputedCity);

                var coordinate = await _weatherApiService.GetCoordinateByCityName(inputedCity.Name);

                _rwOperation.WriteLine("Please enter interested days quantity:");
                var inputedDayQuantity = _rwOperation.ReadLine();
                var dayQuantity = DayQuantityValidation(inputedDayQuantity);

                var cityList = await _weatherApiService.GetFutureTemperatureByCoordinateAndDayQuantity(coordinate, dayQuantity);

                _rwOperation.WriteLine($"{inputedCity.Name} weather forecast:");
                foreach (var city in cityList)
                {
                    _rwOperation.WriteLine($"Day {city.Date.ToString("dd/MM/yyyy")}: " +
                        $"{city.Temperature}. {city.Comment}");
                }
            }

        private City GetCityFromInput()
        {
            var inputedLine = _rwOperation.ReadLine();
            return new City
            {
                Name = inputedLine
            };
        }

        private int DayQuantityValidation(string dayQuantity)
        {
            int day = 0;
            try
            {
                day = Int32.Parse(dayQuantity);
            }
            catch (Exception)
            {
                throw new FormatException("Day quantity should be number");
            }

            int min = Int32.Parse(_configuration["MinForecastDay"]);
            int max = Int32.Parse(_configuration["MaxForecastDay"]);
            if (min > day || max < day)
            {
                throw new NotFoundException("Requested day quantity is not in configuration range");
            }
            return day;

        }
    }
}
