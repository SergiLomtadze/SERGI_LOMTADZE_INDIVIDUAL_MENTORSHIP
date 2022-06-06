using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using ExadelMentorship.Persistence.Context;

namespace ExadelMentorship.Persistence
{
    public class WeatherHistoryRepository : IWeatherHistoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WeatherHistoryRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task SaveInDbAsync(string cityName, double temperature)
        {
            _dbContext.Add(new WeatherHistory
            {
                CityName = cityName,
                Temperature = temperature,
                Time = DateTime.Now
            });
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<WeatherHistory> GetAll()
        {
            return _dbContext.WeatherHistories.AsQueryable();
        }
    }
}
