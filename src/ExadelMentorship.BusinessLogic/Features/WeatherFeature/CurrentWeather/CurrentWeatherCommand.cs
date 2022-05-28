using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Interfaces;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public class CurrentWeatherCommand : ICommand2 <CurrentWeatherCommandResponse>
    {
        public string CityName { get; set; }
    }
}
