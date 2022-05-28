using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using FluentValidation;

namespace ExadelMentorship.BusinessLogic.Validators
{
    public class CurrentWeather : AbstractValidator <CurrentWeatherCommandResponse>
    {
        public CurrentWeather()
        {
            RuleFor(city => city.Name).NotEmpty();
        }
    }
}
