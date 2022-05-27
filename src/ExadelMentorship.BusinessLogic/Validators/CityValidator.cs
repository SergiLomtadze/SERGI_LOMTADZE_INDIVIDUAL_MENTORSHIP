using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using FluentValidation;

namespace ExadelMentorship.BusinessLogic.Validators
{
    public class CityValidator : AbstractValidator <CurrentWeatherCommandResponse>
    {
        public CityValidator()
        {
            RuleFor(city => city.Name).NotEmpty();
        }
    }
}
