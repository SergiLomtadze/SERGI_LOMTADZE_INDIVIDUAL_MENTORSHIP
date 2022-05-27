using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public MaxWeatherCommandHandler(IRWOperation rwOperation, IWeatherApiService weatherApiService, IConfiguration configuration)
        {
            _rwOperation = rwOperation;
            _weatherApiService = weatherApiService;
            _configuration = configuration;
        }
        public async Task Handle(MaxWeatherCommand command)
        {
            var executionTime = _configuration.GetValue<int>("ExecutionMaxTim");
            using CancellationTokenSource tokenSource = new CancellationTokenSource(executionTime*1000);

            var watch = new Stopwatch();
            watch.Start();
            
            _rwOperation.WriteLine("Please enter the cities:");

            var tasks = _rwOperation.ReadLine().Split(',').Select(s => s.Trim())
                .Select(s => _weatherApiService.GetTemperatureByCityNameForMaxTemp(s, tokenSource.Token))
                .Select(async task => await TaskHandling(task));

            await Task.WhenAll(tasks);

            var maxTempCityInfo = tasks.Select(t => t.Result)
                .Where(x => x.Name is not null)
                .OrderByDescending(x => x.Temperature).FirstOrDefault();

            var successfulRequests = tasks.Select(t => t.Result)
                .Where(x => x.ErrorMessage is null && x.IsCancelled is false).Count();

            var failedRequests = tasks.Select(t => t.Result)
                .Where(x => x.ErrorMessage is not null).Count();

            var cancelledRequests = tasks.Select(t => t.Result)
                .Where(x => x.IsCancelled is true).Count();

            if (successfulRequests > 0)
            {
                _rwOperation.WriteLine(Texts.SuccessfulRequest, maxTempCityInfo.Temperature, maxTempCityInfo.Name, successfulRequests, failedRequests, cancelledRequests);
                DebugInfoProvider(tasks);
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
        private void DebugInfoProvider(IEnumerable<Task<MaxTempCityInfo>> tasks)
        {
            var statistic = bool.Parse(_configuration["Statistic"]);
            if (statistic)
            {
                foreach (var item in tasks.Select(t => t.Result))
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
}
