using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces.Weather
{
    public interface IFutureWeatherService
    {
        public Task <Coordinate> GetCoordinateByCityName(string name);
        public Task <List<City>> GetFutureTemperatureByCoordinate(Coordinate coordinate, int day);
    }
}
