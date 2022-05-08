// See https://aka.ms/new-console-template for more information
using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Models;
using System;

while (true) 
{     
    Console.WriteLine("Please enter the city Name:");
    var inputedLine = Console.ReadLine();

    City city = new City()
    {
        Name = inputedLine,
    };
    
    Weather weather = new Weather();
    try
    {
        weather.Validate(city);
        city = await weather.GetTemperatureByCityName(city);
        Console.WriteLine($"In {city.Name} temperature is: {city.Temperature}, {city.Comment}");
    }
    
    catch (NotFoundException exception)
    {
        Console.WriteLine(exception.Message);    
    }

}
