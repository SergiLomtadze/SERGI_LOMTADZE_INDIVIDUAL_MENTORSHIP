using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Interfaces.Weather;
using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Services
{
    public class WeatherSavingService : IWeatherSavingService
    {
        private readonly IWeatherApiService _weatherApiService;
        private IWeatherHistoryRepository _repository;

        public WeatherSavingService(IWeatherApiService weatherApiService, IWeatherHistoryRepository repository)
        {
            _weatherApiService = weatherApiService;
            _repository = repository;
        }
        public async Task SaveInDb(string cityName)
        {
            var temperature = await _weatherApiService.GetTemperatureByCityName(cityName);
            await _repository.AddAsync(new WeatherHistory
            {
                CityName = cityName,
                Temperature = temperature,
                Time = DateTime.Now
            });
            await _repository.SaveAsync();

        }
    }
}
