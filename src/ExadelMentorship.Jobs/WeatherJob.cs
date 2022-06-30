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
            IWeatherApiService weatherApiService, IServiceProvider services)
        {
            _historySettingStorage = historySettingStorage.Value;
            _weatherApiService = weatherApiService;
            _services = services;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {            
            foreach (var item in _historySettingStorage.HistorySettings.ToList())
            {
                string[] time = item.ExecutionTime.Split(":");

                RecurringJob.AddOrUpdate(
                    $"Job_For_{item.City}",
                    () => this.Execute(item.City),
                    $"0 {time[1]} {time[0]} * * ?"
                );
            }
            return Task.CompletedTask;
        }

        public async Task Execute(string cityName)
        {
            using var scope = _services.CreateScope();
            var temp = await _weatherApiService.GetTemperatureByCityName(cityName);
            await scope.ServiceProvider.GetRequiredService<IWeatherHistoryRepository>().SaveAsync(cityName, temp);
        }
    }
}
