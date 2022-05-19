using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.BusinessLogic.Validators;
using FluentValidation.Results;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public static class WeatherHelper
    {
        public static string GetCommentByTemperature(double temperature)
        {
            if (temperature > 30)
            {
                return "It's time to go to the beach";
            }
            else if (temperature > 20)
            {
                return "Good weather";
            }
            else if (temperature > 0)
            {
                return "It's fresh";
            }
            else
            {
                return "Dress warmly";
            }
        }

        public static void ValidateCityName(City city)
        {
            CityValidator validator = new CityValidator();
            ValidationResult results = validator.Validate(city);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    throw new NotFoundException($"City was not inputed");
                }
            }
        }
    }
}
