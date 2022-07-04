using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features.Reports.UserSubscription;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Models;
using FluentValidation.Results;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Services.Weather
{
    public static class WeatherHelperService
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
            Validators.CityValidator validator = new Validators.CityValidator();
            ValidationResult results = validator.Validate(city);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    throw new NotFoundException($"City was not inputed");
                }
            }
        }

        public static void ValidateCurrentWeather(CurrentWeatherCommandResponse response)
        {
            Validators.CurrentWeather validator = new Validators.CurrentWeather();
            ValidationResult results = validator.Validate(response);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    throw new NotFoundException($"City was not inputed");
                }
            }
        }

        public static async Task CityNameExistenceValidation(string name, IHttpClientFactory httpClientFactory)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={name}&appid=7e66067382ed6a093c3e4b6c22940505&units=metric";
            var httpClient = httpClientFactory.CreateClient();
            HttpResponseMessage result = await httpClient.GetAsync(url);

            if ((int)result.StatusCode == 404)
            {
                throw new NotFoundException($"City: {name} was not found");
            }
        }

        public static void ValidateUserSubscription(UserSubscriptionCommand command)
        {
            Validators.UserSubscriptionValidator validator = new Validators.UserSubscriptionValidator();
            ValidationResult results = validator.Validate(command);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    throw new NotFoundException($"Wrong period");
                }
            }
        }
    }
}
