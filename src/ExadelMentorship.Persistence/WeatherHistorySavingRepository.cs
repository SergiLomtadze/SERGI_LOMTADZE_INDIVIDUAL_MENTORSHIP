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

        public async Task SaveInDbAsync(string cityName, double temperature)
        {
            await _dbContext.AddAsync(new WeatherHistory
            {
                CityName = cityName,
                Temperature = temperature,
                Time = DateTime.Now
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}
