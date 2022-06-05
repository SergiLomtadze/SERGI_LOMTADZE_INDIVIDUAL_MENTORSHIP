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

        public void HistorySaving()
        {
            foreach (var item in _historySettingStorage.HistorySettings.ToList())
            {                
                string[] time = item.ExecutionTime.Split(":");

                RecurringJob.AddOrUpdate(
                    $"Job_For_{item.City}",
                    () => Save(item.City),
                    $"0 {time[1]} {time[0]} * * ?"
                );
            }
        }

        private async Task Save(string cityName)
        {
            var temperature = await _weatherApiService.GetTemperatureByCityName(cityName);
            await _weatherHistorySavingRepository.SaveInDbAsync(cityName, temperature);
        }
    }
}
