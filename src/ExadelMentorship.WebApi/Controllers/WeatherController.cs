using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.WebApi.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace ExadelMentorship.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly CommandInvoker _commandInvoker;
        private readonly ILogger<WeatherController> _logger;
        private IWeatherJob _weatherJob;
        public WeatherController(CommandInvoker commandInvoker, ILogger<WeatherController> logger, IWeatherJob weatherJob)
        {
            _commandInvoker = commandInvoker;
            _logger = logger;
            _weatherJob = weatherJob;
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

        public string EnableJobs()
        {
            _weatherJob.HistorySaving();
            return "Jobs Added";
        }
    }
}
