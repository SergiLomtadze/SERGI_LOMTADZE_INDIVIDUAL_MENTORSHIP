using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic
{
    //public class CommandInvoker1
    //{
    //    IRWOperation _rwOperation;
    //    ICurrentWeather _currentWeather;
    //    IFutureWeather _futureWeather;
    //    public CommandInvoker1(IRWOperation rwOperation, ICurrentWeather currentWeather, IFutureWeather futureWeather)
    //    {
    //        _rwOperation = rwOperation;
    //        _currentWeather = currentWeather;
    //        _futureWeather = futureWeather;
    //    }


    //    private int GetActionFromUser()
    //    {
    //        string inputedLine;
    //        do
    //        {
    //            _rwOperation.WriteLine("Please enter Number \n" +
    //                "1 - Current weather \n" +
    //                "2 - Future weather \n" +
    //                "0 - Close");
    //            inputedLine = _rwOperation.ReadLine();
    //        } while (!(inputedLine.Equals("0") || inputedLine.Equals("1") || inputedLine.Equals("2")));
    //        return Convert.ToInt32(inputedLine);
    //    }
    //    private City GetCityFromInput()
    //    {
    //        var inputedLine = _rwOperation.ReadLine();
    //        return new City
    //        {
    //            Name = inputedLine
    //        };
    //    }
    //    public async Task Execute()
    //    {

    //        var action = this.GetActionFromUser();
    //        try
    //        {
    //            if (action == 0)
    //            {
    //                _rwOperation.Close();
    //            }
    //            else if (action == 1)
    //            {
    //                await this.CurrentWeatherExecutor();
    //            }
    //            else 
    //            {
    //                await this.FutureWeatherExecutor();
    //            }
                
    //        }

    //        catch (NotFoundException exception)
    //        {
    //            _rwOperation.WriteLine(exception.Message);
    //        }

    //        _rwOperation.ReadLine();
    //    }

    //    public async Task FutureWeatherExecutor()
    //    {   
    //        _rwOperation.WriteLine("Please enter the city Name:");
    //        var inputedCity = this.GetCityFromInput();

    //        _rwOperation.WriteLine("Please enter interested days quantity:");
    //        var day = _rwOperation.ReadLine();

    //        await _futureWeather.GetCoordinateByCityName(inputedCity.Name);
    //        await _futureWeather.GetFutureTemperatureByCoordinate(Int32.Parse(day));
    //        var cityList = _futureWeather.GetCityList();

    //        _rwOperation.WriteLine($"{inputedCity.Name} weather forecast:");
    //        foreach (var city in cityList)
    //        {
    //            _rwOperation.WriteLine($"Day {city.Date.ToString("dd/MM/yyyy")}: " +
    //                $"{city.Temperature}. {city.Comment}");
    //        }           
    //    }

    //    public async Task CurrentWeatherExecutor()
    //    {
    //        _rwOperation.WriteLine("Please enter the city Name:");
    //        var city = this.GetCityFromInput();

    //        WeatherHelper.ValidateCityName(city);
    //        city.Temperature = await _currentWeather.GetTemperatureByCityName(city.Name);
    //        city.Comment = WeatherHelper.GetCommentByTemperature(city.Temperature);
    //        _rwOperation.WriteLine($"In {city.Name} temperature is: {city.Temperature}, {city.Comment}");
    //    }
    //}
}
