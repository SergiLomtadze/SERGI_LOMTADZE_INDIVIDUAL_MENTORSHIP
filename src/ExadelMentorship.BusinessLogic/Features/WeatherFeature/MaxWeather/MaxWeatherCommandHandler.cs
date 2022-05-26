using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.MaxWeather
{
    public class MaxWeatherCommandHandler : ICommandHandler<MaxWeatherCommand>
    {
        IRWOperation _rwOperation;
        private readonly IWeatherApiService _weatherApiService;
        private readonly IConfiguration _configuration;
        CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public MaxWeatherCommandHandler(IRWOperation rwOperation, IWeatherApiService weatherApiService, IConfiguration configuration)
        {
            _rwOperation = rwOperation;
            _weatherApiService = weatherApiService;
            _configuration = configuration;
        }
        public async Task Handle(MaxWeatherCommand command)
        {
            var token = _tokenSource.Token;
            var executionTime = long.Parse(_configuration["ExecutionMaxTime"]);
            _rwOperation.WriteLine("Please enter the cities:");

            var tasks = _rwOperation.ReadLine().Split(',').Select(s => s.Trim())
                .Select(s => _weatherApiService.GetTemperatureByCityNameForMaxTemp(s, executionTime, token))
                .Select(async task => await TaskHandling(task));

            await Task.WhenAll(tasks);

            var maxTempCityInfo = tasks.Select(t => t.Result)
                .Where(x => x.Name is not null)
                .OrderByDescending(x => x.Temperature).FirstOrDefault();

            var successfulRequests = tasks.Select(t => t.Result)
                .Where(x => x.ErrorMessage is null).Count();

            var failedRequests = tasks.Select(t => t.Result)
                .Where(x => x.ErrorMessage is not null).Count();

            var cancelledRequests = tasks.Select(t => t.Result)
                .Where(x => x.IsCancelled is true).Count();

            if (successfulRequests > 0)
            {
                _rwOperation.WriteLine(Texts.SuccessfulRequest, maxTempCityInfo.Temperature, maxTempCityInfo.Name, successfulRequests, failedRequests, cancelledRequests);
                
                var statistic = bool.Parse(_configuration["Statistic"]);
                if (statistic)
                {
                    DebugInfo(tasks.Select(t => t.Result));      
                }
            }
            else 
            {
                _rwOperation.WriteLine(Texts.NoSuccessful, failedRequests, cancelledRequests);               
            }
        }
        private async Task<MaxTempCityInfo> TaskHandling(Task<MaxTempCityInfo> task)
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
            catch (OperationCanceledException)
            {
                return new MaxTempCityInfo()
                {
                    IsCancelled = true,
                };
            }
        }
        private void DebugInfo(IEnumerable<MaxTempCityInfo> tasks)
        {
            foreach (var item in tasks)
            {
                if (item.Name != null)
                {
                    _rwOperation.WriteLine($"City: {item.Name}. {item.Temperature}. Timer:{item.DurationTime}");
                }
                if (item.ErrorMessage != null)
                {
                    _rwOperation.WriteLine(item.ErrorMessage);
                }
            }
        }
    }
}
