using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.DataAccess;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ExadelMentorship.WebApi.Jobs
{
    public class WeatherJob : BackgroundService
    {
        private HistorySettingStorage _historySettingStorage;
        private IWeatherApiService _weatherApiService;
        private IServiceProvider _services;
        public WeatherJob(IOptions<HistorySettingStorage> historySettingStorage,
            IWeatherApiService weatherApiService,
            IServiceProvider services)
        {
            _historySettingStorage = historySettingStorage.Value;
            _weatherApiService = weatherApiService;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _services.CreateScope();
            foreach (var item in _historySettingStorage.HistorySettings.ToList())
            {
                string[] time = item.ExecutionTime.Split(":");
                var temperature = await _weatherApiService.GetTemperatureByCityName(item.City);
                RecurringJob.AddOrUpdate(
                    $"Job_For_{item.City}",
                    () => scope.ServiceProvider.GetRequiredService<IWeatherHistoryRepository>().SaveInDbAsync(item.City, temperature),
                    $"0 {time[1]} {time[0]} * * ?"
                );
            }
        }
    }
}
