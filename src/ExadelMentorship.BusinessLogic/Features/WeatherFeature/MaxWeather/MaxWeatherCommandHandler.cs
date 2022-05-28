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
    public class MaxWeatherCommandHandler : ICommandHandler<MaxWeatherCommand, MaxWeatherCommandResponse>
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
        public async Task<MaxWeatherCommandResponse> Handle(MaxWeatherCommand maxWeather)
        {
            MaxWeatherCommandResponse response = new MaxWeatherCommandResponse();
            var executionTime = _configuration.GetValue<int>("ExecutionMaxTime");
            using CancellationTokenSource tokenSource = new CancellationTokenSource(executionTime * 1000);           
            
            var tasks = maxWeather.Cities.Split(',').Select(s => s.Trim())
                .Select(s => _weatherApiService.GetTemperatureByCityNameForMaxTemp(s, tokenSource.Token))
                .Select(async task => await TaskHandling(task));

            await Task.WhenAll(tasks);

            response.MaxTempCityInfo = tasks.Select(t => t.Result)
                .Where(x => x.Name is not null)
                .OrderByDescending(x => x.Temperature).FirstOrDefault();

            response.SuccessfulRequests = tasks.Select(t => t.Result)
                .Where(x => x.ErrorMessage is null && x.IsCancelled is false).Count();

            response.FailedRequests = tasks.Select(t => t.Result)
                .Where(x => x.ErrorMessage is not null).Count();

            response.CancelledRequests = tasks.Select(t => t.Result)
                .Where(x => x.IsCancelled is true).Count();

            response.MaxTempCityInfoList = tasks.Select(t => t.Result);

            response.Statistic = bool.Parse(_configuration["Statistic"]);

            return response;
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
    }
}
