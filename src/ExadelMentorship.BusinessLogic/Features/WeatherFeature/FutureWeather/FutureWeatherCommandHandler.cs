using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Interfaces.Weather;
using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather
{
    public class FutureWeatherCommandHandler : ICommandHandler<FutureWeatherCommand>
    {
        IRWOperation _rwOperation;
        private readonly IFutureWeatherService _futureWeatherService;
        public FutureWeatherCommandHandler(IRWOperation rwOperation, IFutureWeatherService futureWeatherService)
        {
            _rwOperation = rwOperation;
            _futureWeatherService = futureWeatherService;
        }

        public async Task Handle(FutureWeatherCommand futureWeather)
            {
                _rwOperation.WriteLine("Please enter the city Name:");
                var inputedCity = this.GetCityFromInput();
                await _futureWeatherService.GetCoordinateByCityName(inputedCity.Name);

                _rwOperation.WriteLine("Please enter interested days quantity:");
                var day = _rwOperation.ReadLine();
                DayQuantityValidation(day);


                await _futureWeatherService.GetFutureTemperatureByCoordinate(Int32.Parse(day));
                var cityList = _futureWeatherService.GetCityList();

                _rwOperation.WriteLine($"{inputedCity.Name} weather forecast:");
                foreach (var city in cityList)
                {
                    _rwOperation.WriteLine($"Day {city.Date.ToString("dd/MM/yyyy")}: " +
                        $"{city.Temperature}. {city.Comment}");
                }
            }

        private City GetCityFromInput()
            {
                var inputedLine = Console.ReadLine();
                return new City
                {
                    Name = inputedLine
                };
            }

        private void DayQuantityValidation(string dayQuantity)
            {
                try
                {
                    var day = Int32.Parse(dayQuantity);
                    int min = Int32.Parse(_configuration["MinForecastDay"]);
                    int max = Int32.Parse(_configuration["MaxForecastDay"]);
                    if (min > day || max < day)
                    {
                        throw new NotFoundException("Requested day quantity is not in configuration range");
                    }
                }
                catch (Exception ex)
                {

                    throw new NotFoundException(ex.ToString());
                }
            }
    }
}
