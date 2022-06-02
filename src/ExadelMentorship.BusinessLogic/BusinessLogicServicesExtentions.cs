using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Interfaces.Weather;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ExadelMentorship.BusinessLogic
{
    public static class BusinessLogicServicesExtentions
    {
        public static void AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddSingleton<CommandInvoker>();
        
            services.AddSingleton<IWeatherApiService, WeatherApiService>();
        
            services.AddSingleton<ICommandHandler<CurrentWeatherCommand, CurrentWeatherCommandResponse>, CurrentWeatherCommandHandler>();
        
            services.AddSingleton<ICommandHandler<FutureWeatherCommand, IEnumerable<City>>, FutureWeatherCommandHandler>();           

            services.AddHttpClient();

            services.AddOptions<ForecastDaySettings>().BindConfiguration(nameof(ForecastDaySettings));

            services.AddOptions<MaxWeatherSettings>().BindConfiguration(nameof(MaxWeatherSettings));

            services.AddScoped<IWeatherSavingService, WeatherSavingService>();
        }
    }
}
