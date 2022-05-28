using ExadelMentorship.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather
{
    public class FutureWeatherCommand : ICommand2<FutureWeatherCommandResponse>
    {
        public string CityName { get; set; }
        public string DayQuantity { get; set; }
    }
}
