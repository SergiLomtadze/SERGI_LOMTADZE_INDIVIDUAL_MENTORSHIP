using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces.Weather
{
    public interface IFutureWeatherService
    {
        public Task GetCoordinateByCityName(string name);
        public Task GetFutureTemperatureByCoordinate(int day);
        public List<City> GetCityList();
    }
}
