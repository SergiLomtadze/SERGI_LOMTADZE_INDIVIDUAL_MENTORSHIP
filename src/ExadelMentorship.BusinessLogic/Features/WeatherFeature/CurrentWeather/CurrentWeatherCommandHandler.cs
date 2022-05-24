using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public class CurrentWeatherCommandHandler : ICommandHandler<CurrentWeatherCommand>
    {
        private readonly IRWOperation _rwOperation;
        private readonly IWeatherApiService _weatherApiService;
        public CurrentWeatherCommandHandler(IRWOperation rwOperation, IWeatherApiService weatherApiService)
        {
            _rwOperation = rwOperation;
            _weatherApiService = weatherApiService;
        }

        public async Task Handle(CurrentWeatherCommand currentWeatherCommand)
        {
            _rwOperation.WriteLine("Please enter the city Name:");
            var city = this.GetCityFromInput();

            WeatherHelper.ValidateCityName(city);
            city.Temperature = await _weatherApiService.GetTemperatureByCityName(city.Name);
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
