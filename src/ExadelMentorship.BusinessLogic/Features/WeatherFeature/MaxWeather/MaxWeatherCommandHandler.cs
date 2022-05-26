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

            var tasks = _rwOperation.ReadLine().Split(',').Select(s => s.Trim())
                .Select(s => _weatherApiService.GetTemperatureByCityNameForMaxTemp(s))
                .Select(async task =>
                    {
                        try
                        {
                            return await task;
                        }
                        catch (NotFoundException ex)
                        {
                            return new MaxTempCityInfo()
                            {
                                ErrorMessage = ex.Message,
                            };
                        }
                    });

            await Task.WhenAll(tasks);

            var maxTempCityInfo = tasks.Select(t => t.Result)
                .Where(x => x.Name is not null)
                .OrderByDescending(x => x.Temperature).FirstOrDefault();

            var successfulRequests = tasks.Select(t => t.Result)
                .Where(x => x.ErrorMessage is null).Count();

            var failedRequests = tasks.Select(t => t.Result)
                .Where(x => x.ErrorMessage is not null).Count();

            if (successfulRequests > 0)
            {
                _rwOperation.WriteLine(Texts.S1, maxTempCityInfo.Temperature, maxTempCityInfo.Name, successfulRequests, failedRequests);
                
                var statistic = bool.Parse(_configuration["Statistic"]);
                if (statistic)
                {
                    _rwOperation.WriteLine($"City: {maxTempCityInfo.Name}. {maxTempCityInfo.Temperature}. Timer:{maxTempCityInfo.DurationTime}");
                }
            }
            else 
            {
                _rwOperation.WriteLine($" Error, no successful requests.Failed requests count: {failedRequests}");               
            }
        }
    }
}
