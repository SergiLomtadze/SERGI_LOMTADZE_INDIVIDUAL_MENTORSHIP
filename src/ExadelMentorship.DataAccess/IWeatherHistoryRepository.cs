using ExadelMentorship.DataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ExadelMentorship.DataAccess
{
    public interface IWeatherHistoryRepository
    {
        IQueryable<WeatherHistory> GetAll();
        Task SaveInDbAsync(string cityName, double temperature);
    }
}
