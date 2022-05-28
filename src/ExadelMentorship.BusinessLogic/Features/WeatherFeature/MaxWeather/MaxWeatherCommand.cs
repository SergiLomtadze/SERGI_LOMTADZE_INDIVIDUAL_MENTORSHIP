using ExadelMentorship.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.MaxWeather
{
    public class MaxWeatherCommand : ICommand2<MaxWeatherCommandResponse>
    {
        public string Cities { get; set; }
    }
}
