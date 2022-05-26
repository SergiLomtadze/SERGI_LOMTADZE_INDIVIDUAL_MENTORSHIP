using ExadelMentorship.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces
{
    public interface IWeatherApiService
    {
        public Task<double> GetTemperatureByCityName(string name);
        public Task<Coordinate> GetCoordinateByCityName(string name);
        public Task<List<City>> GetFutureTemperatureByCoordinateAndDayQuantity(Coordinate coordinate, int day);
        public Task<MaxTempCityInfo> GetTemperatureByCityNameForMaxTemp(string name);
    }
}
