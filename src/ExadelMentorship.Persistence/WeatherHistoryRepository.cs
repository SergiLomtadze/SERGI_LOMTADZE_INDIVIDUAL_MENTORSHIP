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

        public async Task AddAsync(WeatherHistory entity)
        {            
            await _dbContext.WeatherHistories.AddAsync(entity);            
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
