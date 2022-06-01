using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather
{
    public class FutureWeatherCommandHandler : ICommandHandler<FutureWeatherCommand, IEnumerable<City>>
    {
        private readonly IWeatherApiService _weatherApiService;
        private ForecastDaySettings _forecastDayInfo;
        public FutureWeatherCommandHandler(IWeatherApiService weatherApiService, IOptions<ForecastDaySettings> forecastDayInfo)
        {
            _weatherApiService = weatherApiService;
            _forecastDayInfo = forecastDayInfo.Value;
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

            int min = _forecastDayInfo.MaxForecastDay;
            int max = _forecastDayInfo.MinForecastDay;
            if (min > day || max < day)
            {
                throw new NotFoundException("Requested day quantity is not in configuration range");
            }
            return day;

        }
    }
}
