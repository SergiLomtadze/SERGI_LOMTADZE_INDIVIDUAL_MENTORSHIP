﻿using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Interfaces.Weather;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExadelMentorship.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly CommandInvoker _commandInvoker;
        private IWeatherSavingService _weatherSaving;
        public WeatherController(CommandInvoker commandInvoker, IWeatherSavingService weatherSaving)
        {
            _commandInvoker = commandInvoker;
            _weatherSaving = weatherSaving;
        }

        [HttpGet("currentWeather/{cityName}")]
        public Task<CurrentWeatherCommandResponse> GetCurrentWeather([FromRoute] string cityName)
        {
            return _commandInvoker.Invoke
            (
                new CurrentWeatherCommand
                {
                    CityName = cityName
                }
            );
        }

        [HttpGet("currentWeather/{cityName}/days/{dayQuantity}")]
        public Task<IEnumerable<City>> GetFutureWeather([FromRoute] string cityName, [FromRoute] string dayQuantity)
        {
            return _commandInvoker.Invoke
            (
                new FutureWeatherCommand
                {
                    CityName = cityName,
                    DayQuantity = dayQuantity
                }
            );
        }
    }
}
