﻿using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Interfaces.Weather;
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
                .AddSingleton<ICommandHandler<CurrentWeatherCommand>, CurrentWeatherCommandHandler>()
                .AddSingleton<ICurrentWeatherService, CurrentWeatherService>()
                .AddSingleton<ICommandHandler<FutureWeatherCommand>, FutureWeatherCommandHandler>()
                .AddSingleton<IFutureWeatherService, FutureWeatherService>()
                .AddSingleton<IRWOperation, ConsoleOperation>()
                .AddHttpClient()
                .BuildServiceProvider()
                .GetRequiredService<T>();
        }
    }
}
