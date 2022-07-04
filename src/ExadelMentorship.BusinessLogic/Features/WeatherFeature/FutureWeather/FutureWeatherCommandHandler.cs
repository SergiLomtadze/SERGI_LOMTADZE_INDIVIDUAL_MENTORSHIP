using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using ExadelMentorship.BusinessLogic.Services.Weather;

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
            WeatherHelperService.ValidateCityName(new City { Name = futureWeather.CityName });
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
                throw new ValidationException("Day quantity should be number");
            }

            int min = _forecastDayInfo.MinForecastDay;
            int max = _forecastDayInfo.MaxForecastDay;
            if (min > day || max < day)
            {
                throw new ValidationException("Requested day quantity is not in configuration range");
            }
            return day;

        }
    }
}
