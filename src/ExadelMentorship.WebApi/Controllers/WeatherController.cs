using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExadelMentorship.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly CommandInvoker _commandInvoker;
        public WeatherController(CommandInvoker commandInvoker)
        {
            _commandInvoker = commandInvoker;
        }

        [HttpGet("currentWeather/{cityName}")]
        public async Task<ActionResult<CurrentWeatherCommandResponse>> GetCurrentWeather([FromRoute] string cityName)
        {
            var result = await _commandInvoker.Invoke
            (
                new CurrentWeatherCommand
                {
                    CityName = cityName
                }
            );
            return Ok(result);
        }
    }
}
