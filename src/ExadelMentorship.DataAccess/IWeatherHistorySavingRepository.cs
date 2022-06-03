using ExadelMentorship.DataAccess.Entities;
using System.Threading.Tasks;

namespace ExadelMentorship.DataAccess
{
    public interface IWeatherHistorySavingRepository
    {
        Task SaveInDbAsync(string cityName, double temperature);
    }
}
