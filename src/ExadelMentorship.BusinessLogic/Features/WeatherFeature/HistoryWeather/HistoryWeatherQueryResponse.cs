using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.HistoryWeather
{
    public class HistoryWeatherQueryResponse
    {
        public string CityName { get; set; }
        public double Temperature { get; set; }
        public DateTime Time { get; set; }
    }
}
