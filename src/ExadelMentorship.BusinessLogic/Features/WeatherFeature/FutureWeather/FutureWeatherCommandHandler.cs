using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather
{
    public class FutureWeatherCommandHandler : ICommandHandler2<FutureWeatherCommand, FutureWeatherCommandResponse>
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

        public async Task<FutureWeatherCommandResponse> Handle(FutureWeatherCommand futureWeather)
        {
            WeatherHelper.ValidateCityName(new City { Name = futureWeather.CityName });
            var coordinate = await _weatherApiService.GetCoordinateByCityName(futureWeather.CityName);
            var dayQuantity = DayQuantityValidation(futureWeather.DayQuantity);
            
            return new FutureWeatherCommandResponse
            { 
                cityList = await _weatherApiService.GetFutureTemperatureByCoordinateAndDayQuantity(coordinate, dayQuantity)
            };
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
