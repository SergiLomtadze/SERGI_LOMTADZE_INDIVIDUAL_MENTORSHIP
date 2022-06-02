using ExadelMentorship.DataAccess.Entities;
using System.Threading.Tasks;

namespace ExadelMentorship.DataAccess
{
    public interface IWeatherHistoryRepository
    {
        Task AddAsync(WeatherHistory entity);
        Task SaveAsync();
    }
}
