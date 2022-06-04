using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.DataAccess;
using Hangfire;
using Microsoft.Extensions.Options;


namespace ExadelMentorship.WebApi.Jobs
{
    public class WeatherJob : IWeatherJob
    {
        private HistorySettingStorage _historySettingStorage;
        private IWeatherApiService _weatherApiService;
        private IWeatherHistorySavingRepository _weatherHistorySavingRepository;
        public WeatherJob(IOptions<HistorySettingStorage> historySettingStorage,
            IWeatherApiService weatherApiService,
            IWeatherHistorySavingRepository weatherHistorySavingRepository)
        {
            _historySettingStorage = historySettingStorage.Value;
            _weatherApiService = weatherApiService;
            _weatherHistorySavingRepository = weatherHistorySavingRepository;
        }

        public async Task HistorySaving()
        {
            foreach (var item in _historySettingStorage.HistorySettings.ToList())
            {
                var temperature = await _weatherApiService.GetTemperatureByCityName(item.City);
                string []time = item.ExecutionTime.Split(":");

                RecurringJob.AddOrUpdate(
                    $"Job_For_{item.City}",
                    () => _weatherHistorySavingRepository.SaveInDb(item.City, temperature),
                    $"0 {time[1]} {time[0]} * * ?"
                );                
            }
        }
    }
}
