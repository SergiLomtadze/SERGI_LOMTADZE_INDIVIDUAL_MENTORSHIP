using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather
{
    public class FutureWeatherCommandResponse
    {
        public IEnumerable<City> cityList { get; set; }
    }
}
