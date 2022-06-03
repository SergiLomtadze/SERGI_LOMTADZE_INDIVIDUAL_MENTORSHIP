using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using ExadelMentorship.Persistence.Context;

namespace ExadelMentorship.Persistence
{
    public class WeatherHistorySavingRepository : IWeatherHistorySavingRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWeatherApiService _weatherApiService;

        public WeatherHistorySavingRepository(ApplicationDbContext context, IWeatherApiService weatherApiService)
        {
            _dbContext = context;
            _weatherApiService = weatherApiService;
        }

        public async Task SaveByCityNameAsync(string cityName)
        {
            var temperature = await _weatherApiService.GetTemperatureByCityName(cityName);
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
