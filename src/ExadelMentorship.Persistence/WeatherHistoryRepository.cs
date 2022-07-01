using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using ExadelMentorship.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace ExadelMentorship.Persistence
{
    public class WeatherHistoryRepository : IWeatherHistoryRepository
    {
        private IServiceProvider _services;

        public WeatherHistoryRepository(IServiceProvider services)
        {
            _services = services;
        }

        public async Task SaveAsync(string cityName, double temperature)
        {
            var scope = _services.CreateScope();
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().WeatherHistories.Add(new WeatherHistory
            {
                CityName = cityName,
                Temperature = temperature,
                Time = DateTime.Now
            });
            await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().SaveChangesAsync();
        }

        public IQueryable<WeatherHistory> GetByCity(string name)
        {
            var scope = _services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<ApplicationDbContext>()
                .WeatherHistories.Where(x => x.CityName == name);
        }
    }
}
