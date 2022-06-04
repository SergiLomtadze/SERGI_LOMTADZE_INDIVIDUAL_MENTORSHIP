using ExadelMentorship.DataAccess.Entities;
using System.Threading.Tasks;

namespace ExadelMentorship.DataAccess
{
    public interface IWeatherHistorySavingRepository
    {
        void SaveInDb(string cityName, double temperature);
    }
}
