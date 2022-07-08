using ExadelMentorship.DataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ExadelMentorship.DataAccess
{
    public interface IWeatherHistoryRepository
    {
        IQueryable<WeatherHistory> GetByCity(string name);
        Task SaveAsync(string cityName, double temperature);        
    }
}
