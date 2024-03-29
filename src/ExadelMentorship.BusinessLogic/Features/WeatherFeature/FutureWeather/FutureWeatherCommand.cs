﻿using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather
{
    public class FutureWeatherCommand : ICommand<IEnumerable<City>>
    {
        public string CityName { get; set; }
        public string DayQuantity { get; set; }
    }
}
