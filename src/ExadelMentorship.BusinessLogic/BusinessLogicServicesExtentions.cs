using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ExadelMentorship.BusinessLogic
{
    public static class BusinessLogicServicesExtentions
    {
        public static void AddBlServices(this IServiceCollection services)
        {
            services.AddSingleton<CommandInvoker>();
        
            services.AddSingleton<IWeatherApiService, WeatherApiService>();
        
            services.AddSingleton<ICommandHandler<CurrentWeatherCommand, CurrentWeatherCommandResponse>, CurrentWeatherCommandHandler>();
        
            services.AddSingleton<ICommandHandler<FutureWeatherCommand, IEnumerable<City>>, FutureWeatherCommandHandler>();           

            services.AddHttpClient();
        }
    }
}
