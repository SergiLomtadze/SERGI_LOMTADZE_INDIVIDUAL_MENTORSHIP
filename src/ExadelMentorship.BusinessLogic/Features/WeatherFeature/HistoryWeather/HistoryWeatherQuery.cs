using ExadelMentorship.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.HistoryWeather
{
    public class HistoryWeatherQuery : ICommand<IEnumerable<HistoryWeatherQueryResponse>>
    {
        public string CityName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
