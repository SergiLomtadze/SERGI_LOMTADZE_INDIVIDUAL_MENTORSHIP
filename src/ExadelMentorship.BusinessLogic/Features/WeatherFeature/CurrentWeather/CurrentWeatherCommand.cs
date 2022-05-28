using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Interfaces;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public class CurrentWeatherCommand : ICommand <CurrentWeatherCommandResponse>
    {
        public string CityName { get; set; }
    }
}
