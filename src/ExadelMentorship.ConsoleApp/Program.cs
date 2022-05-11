// See https://aka.ms/new-console-template for more information
using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Net.Http;

while (true) 
{     
    Console.WriteLine("Please enter the city Name:");
    var inputedLine = Console.ReadLine();

    City city = new City
    {
        Name = inputedLine
    };


    using (HttpClient httpClient = new HttpClient())
    {
        try
        {
            Weather weather = new Weather(httpClient);
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
}
