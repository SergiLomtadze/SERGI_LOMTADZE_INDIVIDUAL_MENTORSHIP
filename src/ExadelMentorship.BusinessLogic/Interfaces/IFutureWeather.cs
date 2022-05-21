using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces
{
    public interface IFutureWeather
    {
        public List<City> GetCityList();
        public Task GetCoordinateByCityName(string name);
        public Task GetFutureTemperatureByCoordinate(int day);

    }
}
