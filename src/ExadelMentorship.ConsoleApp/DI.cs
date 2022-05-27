﻿using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                .AddSingleton<MainJob>()
                .AddSingleton<CommandInvoker>()
                .AddSingleton<IWeatherApiService, WeatherApiService>()
                .AddSingleton<ICommandHandler<CurrentWeatherCommand, CurrentWeatherCommandResponse>, CurrentWeatherCommandHandler>()
                //                .AddSingleton<ICommandHandler<CurrentWeatherCommand>, CurrentWeatherCommandHandler>()
                //                .AddSingleton<ICommandHandler<FutureWeatherCommand>, FutureWeatherCommandHandler>()
                //                .AddSingleton<ICommandHandler<MaxWeatherCommand>, MaxWeatherCommandHandler>()
                .AddSingleton<IRWOperation, ConsoleOperation>()
                .AddHttpClient()
                .BuildServiceProvider()
                .GetRequiredService<T>();
        }
    }
}
