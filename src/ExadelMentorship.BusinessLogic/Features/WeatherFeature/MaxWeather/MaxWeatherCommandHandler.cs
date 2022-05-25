using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.MaxWeather
{
    public class MaxWeatherCommandHandler : ICommandHandler<MaxWeatherCommand>
    {
        IRWOperation _rwOperation;
        private readonly IWeatherApiService _weatherApiService;
        private readonly IConfiguration _configuration;

        public MaxWeatherCommandHandler(IRWOperation rwOperation, IWeatherApiService weatherApiService, IConfiguration configuration)
        {
            _rwOperation = rwOperation;
            _weatherApiService = weatherApiService;
            _configuration = configuration;
        }

        public async Task Handle(MaxWeatherCommand command)
        {
            _rwOperation.WriteLine("Please enter the cities:");
            var cities = this.GetCitiesFromInput();
            try
            {
                var tasks = new List<Task<MaxTempCityInfo>>();
                foreach (var city in cities)
                {
                    tasks.Add(_weatherApiService.GetTemperatureByCityNameForMaxTemp(city));
                }

                await Task.WhenAll(tasks);
                var maxTempCityInfoList = new List<MaxTempCityInfo>();
                foreach (var task in tasks)
                {
                    maxTempCityInfoList.Add(await task);
                }

                var maxTempCityInfo = maxTempCityInfoList.OrderByDescending(x => x.Temperature).First();

                _rwOperation.WriteLine($"City with the highest temperature {maxTempCityInfo.Temperature} C: {maxTempCityInfo.Name}");

                var statistic = bool.Parse(_configuration["Statistic"]);
                if (statistic)
                {
                    _rwOperation.WriteLine($"City: {maxTempCityInfo.Name}. {maxTempCityInfo.Temperature}. Timer:{maxTempCityInfo.DurationTime}");
                }
            }
            catch (NotFoundException ex)
            {
                _rwOperation.WriteLine(ex.Message);
            }

                
        }

        private IEnumerable<string> GetCitiesFromInput()
        {
            var cities = new List<string>();
            var inputedLine = _rwOperation.ReadLine();
            string[] words = inputedLine.Split(',');
            foreach (string word in words)
            {
                cities.Add(word.Trim());
            }
            return cities;
        }
    }
}
