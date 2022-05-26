using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

            try
            {
                var tasks = _rwOperation.ReadLine().Split(',').Select(s => s.Trim())
                    .Select(s => _weatherApiService.GetTemperatureByCityNameForMaxTemp(s));

                await Task.WhenAll(tasks);

                var maxTempCityInfo = tasks.Select( t =>  t.Result).OrderByDescending(x => x.Temperature).First();

                _rwOperation.WriteLine(Texts.S1, maxTempCityInfo.Temperature, maxTempCityInfo.Name);

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
    }
}
