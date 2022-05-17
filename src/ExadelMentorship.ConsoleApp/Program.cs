// See https://aka.ms/new-console-template for more information
using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

//DI setup/release 
using var serviceProvider = new ServiceCollection()
    .AddSingleton<Weather>()
    .AddSingleton<ConsoleJob>()
    .AddSingleton<IConsoleOperation, ConsoleOperation>()
    .AddHttpClient()
    .BuildServiceProvider();
    
//DI configure
var weather = serviceProvider.GetRequiredService<Weather>();
var consoleJob = serviceProvider.GetRequiredService<ConsoleJob>();

await consoleJob.DoJob(weather);


