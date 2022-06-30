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

        public async Task SaveAsync(string cityName, double temperature)
        {
            _dbContext.Add(new WeatherHistory
            {
                CityName = cityName,
                Temperature = temperature,
                Time = DateTime.Now
            });
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<WeatherHistory> GetByCity(string name)
        {
            return _dbContext.WeatherHistories.Where(x => x.CityName == name);
        }
    }
}
