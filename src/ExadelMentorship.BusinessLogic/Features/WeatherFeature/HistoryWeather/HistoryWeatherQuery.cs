using ExadelMentorship.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.HistoryWeather
{
    public class HistoryWeatherQuery : ICommand<HistoryWeatherQueryResponse>
    {
        public string CityName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
