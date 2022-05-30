using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.MaxWeather;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic
{
    public static class DependencyInjection
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
