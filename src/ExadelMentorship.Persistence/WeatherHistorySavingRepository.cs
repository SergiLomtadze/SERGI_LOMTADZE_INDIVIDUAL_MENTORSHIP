using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using ExadelMentorship.Persistence.Context;

namespace ExadelMentorship.Persistence
{
    public class WeatherHistorySavingRepository : IWeatherHistorySavingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WeatherHistorySavingRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void SaveInDb(string cityName, double temperature)
        {
            _dbContext.Add(new WeatherHistory
            {
                CityName = cityName,
                Temperature = temperature,
                Time = DateTime.Now
            });
            _dbContext.SaveChanges();
        }
    }
}
