using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.HistoryWeather;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExadelMentorship.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly CommandInvoker _commandInvoker;
        private readonly ILogger<WeatherController> _logger;
        public WeatherController(CommandInvoker commandInvoker, ILogger<WeatherController> logger)
        {
            _commandInvoker = commandInvoker;
            _logger = logger;
        }

        [HttpGet("currentWeather/{cityName}")]
        public Task<CurrentWeatherCommandResponse> GetCurrentWeather([FromRoute] string cityName)
        {
            _logger.LogInformation($"requested city for current weather: {cityName}");
            return _commandInvoker.Invoke
            (
                new CurrentWeatherCommand
                {
                    CityName = cityName
                }
            );
        }

        [Authorize(Roles = "User")]
        [HttpGet("historyWeather/{cityName}")]
        public Task<IEnumerable<HistoryWeatherQueryResponse>> GetHistoryWeatherByCity(
            [FromRoute] string cityName,
            DateTime from,
            DateTime to
            )
        {
            _logger.LogInformation($"requested city for history weather: {cityName}");

            return _commandInvoker.Invoke
            (
                new HistoryWeatherQuery
                {
                    CityName = cityName,
                    From = from,
                    To = to
                }
            );
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("currentWeather/{cityName}/days/{dayQuantity}")]
        public Task<IEnumerable<City>> GetFutureWeather([FromRoute] string cityName, [FromRoute] string dayQuantity)
        {
            _logger.LogInformation($"requested city for future weather: {cityName}, requested days quantity: {dayQuantity}");
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
