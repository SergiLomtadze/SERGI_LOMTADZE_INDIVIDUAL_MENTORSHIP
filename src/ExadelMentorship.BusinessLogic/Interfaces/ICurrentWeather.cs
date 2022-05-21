﻿using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces
{
    public interface ICurrentWeather
    {
        public Task<double> GetTemperatureByCityName(string name);
    }
}
