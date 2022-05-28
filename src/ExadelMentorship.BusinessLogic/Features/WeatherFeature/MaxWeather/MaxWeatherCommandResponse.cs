using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.MaxWeather
{
    public class MaxWeatherCommandResponse
    {
        public MaxTempCityInfo MaxTempCityInfo { get; set; }
        public int SuccessfulRequests { get; set; }
        public int FailedRequests { get; set; }
        public int CancelledRequests { get; set; }
        public IEnumerable<MaxTempCityInfo> MaxTempCityInfoList { get; set; }
        public bool Statistic { get; set; }

    }
}
