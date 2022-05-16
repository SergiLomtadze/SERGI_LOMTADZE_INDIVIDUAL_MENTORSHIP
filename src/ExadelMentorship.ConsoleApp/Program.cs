// See https://aka.ms/new-console-template for more information
using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;


using var serviceProvider = new ServiceCollection()
    .AddSingleton<Weather>()
    .AddSingleton<HttpClient>()
    .BuildServiceProvider();

var weather = serviceProvider.GetRequiredService<Weather>();

while (true) 
{     
    Console.WriteLine("Please enter the city Name:");
    var inputedLine = Console.ReadLine();

    City city = new City
    {
        Name = inputedLine
    };

    try
    {
        weather.ValidateCityName(city);
        city.Temperature = await weather.GetTemperatureByCityName(city.Name);
        city.Comment = WeatherHelper.GetCommentByTemperature(city.Temperature);
        Console.WriteLine($"In {city.Name} temperature is: {city.Temperature}, {city.Comment}");
    }
    catch (NotFoundException exception)
    {
        Console.WriteLine(exception.Message);
    }

}
