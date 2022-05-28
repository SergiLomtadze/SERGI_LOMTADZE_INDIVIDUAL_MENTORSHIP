﻿using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.MaxWeather;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ExadelMentorship.IntegrationTests
{
    public class DI
    {
        public static T Resolve<T>()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            return new ServiceCollection()
                .AddSingleton(config)
                .AddSingleton<MainJob2>()
                .AddSingleton<CommandInvoker2>()
                .AddSingleton<IWeatherApiService, WeatherApiService>()
                .AddSingleton<ICommandHandler2<CurrentWeatherCommand, CurrentWeatherCommandResponse>, CurrentWeatherCommandHandler>()               
                .AddSingleton<ICommandHandler2<FutureWeatherCommand, IEnumerable<City>>, FutureWeatherCommandHandler>()
                .AddSingleton<ICommandHandler2<MaxWeatherCommand, MaxWeatherCommandResponse>, MaxWeatherCommandHandler>()
                .AddSingleton<IRWOperation, ConsoleOperation>()
                .AddHttpClient()
                .BuildServiceProvider()
                .GetRequiredService<T>();
        }
    }
}
