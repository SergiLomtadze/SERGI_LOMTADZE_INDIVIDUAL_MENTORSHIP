using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces
{
    public interface IWeather
    {
        public void ValidateCityName(City city);
        public Task<double> GetTemperatureByCityName(string name);
    }
}
