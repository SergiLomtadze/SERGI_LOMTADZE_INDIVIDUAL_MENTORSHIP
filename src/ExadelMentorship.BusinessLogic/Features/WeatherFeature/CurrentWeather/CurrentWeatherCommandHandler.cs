using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Services.Weather;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public class CurrentWeatherCommandHandler : ICommandHandler<CurrentWeatherCommand, CurrentWeatherCommandResponse>
    {
        private readonly IWeatherApiService _weatherApiService;
        public CurrentWeatherCommandHandler(IWeatherApiService weatherApiService)
        {
            _weatherApiService = weatherApiService;
        }

        public async Task<CurrentWeatherCommandResponse> Handle(CurrentWeatherCommand currentWeatherCommand)
        {
            CurrentWeatherCommandResponse response = new CurrentWeatherCommandResponse();
            response.Name = currentWeatherCommand.CityName;

            WeatherHelperService.ValidateCurrentWeather(response);
            response.Temperature = await _weatherApiService.GetTemperatureByCityName(response.Name);
            response.Comment = WeatherHelperService.GetCommentByTemperature(response.Temperature);            
            
            return response;
        }
    }
}
