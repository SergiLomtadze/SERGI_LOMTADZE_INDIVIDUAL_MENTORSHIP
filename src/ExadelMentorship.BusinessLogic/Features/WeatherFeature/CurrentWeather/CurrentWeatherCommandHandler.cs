using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Interfaces;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public class CurrentWeatherCommandHandler : ICommandHandler2<CurrentWeatherCommand, CurrentWeatherCommandResponse>
    {
        private readonly IRWOperation _rwOperation;
        private readonly IWeatherApiService _weatherApiService;
        public CurrentWeatherCommandHandler(IRWOperation rwOperation, IWeatherApiService weatherApiService)
        {
            _rwOperation = rwOperation;
            _weatherApiService = weatherApiService;
        }

        public async Task<CurrentWeatherCommandResponse> Handle(CurrentWeatherCommand currentWeatherCommand)
        {
            CurrentWeatherCommandResponse response = new CurrentWeatherCommandResponse();
            response.Name = currentWeatherCommand.CityName;

            WeatherHelper.ValidateCurrentWeather(response);
            response.Temperature = await _weatherApiService.GetTemperatureByCityName(response.Name);
            response.Comment = WeatherHelper.GetCommentByTemperature(response.Temperature);            
            
            return response;
        }
    }
}
