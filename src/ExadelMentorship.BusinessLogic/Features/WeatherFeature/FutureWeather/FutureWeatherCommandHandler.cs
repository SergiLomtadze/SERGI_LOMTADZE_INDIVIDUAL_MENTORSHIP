using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather
{
    public class FutureWeatherCommandHandler : ICommandHandler<FutureWeatherCommand, IEnumerable<City>>
    {
        private readonly IWeatherApiService _weatherApiService;
        private readonly IConfiguration _configuration;
        public FutureWeatherCommandHandler(IWeatherApiService weatherApiService, IConfiguration configuration)
        {
            _weatherApiService = weatherApiService;
            _configuration = configuration;
        }

        public async Task<IEnumerable<City>> Handle(FutureWeatherCommand futureWeather)
        {
            WeatherHelper.ValidateCityName(new City { Name = futureWeather.CityName });
            var coordinate = await _weatherApiService.GetCoordinateByCityName(futureWeather.CityName);
            var dayQuantity = DayQuantityValidation(futureWeather.DayQuantity);

            return await _weatherApiService.GetFutureTemperatureByCoordinateAndDayQuantity(coordinate, dayQuantity, futureWeather.CityName);
        }

        private int DayQuantityValidation(string dayQuantity)
        {
            int day = 0;
            try
            {
                day = int.Parse(dayQuantity);
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
