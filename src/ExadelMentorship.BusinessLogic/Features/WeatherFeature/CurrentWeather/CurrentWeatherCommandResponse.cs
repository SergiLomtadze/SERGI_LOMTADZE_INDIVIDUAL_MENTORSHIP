using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather
{
    public class CurrentWeatherCommandResponse
    {
        public string Name { get; set; }
        public double Temperature { get; set; }
        public string Comment { get; set; }
    }
}
