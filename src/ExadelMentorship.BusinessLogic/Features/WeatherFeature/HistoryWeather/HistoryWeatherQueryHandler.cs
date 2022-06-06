using ExadelMentorship.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.HistoryWeather
{
    public class HistoryWeatherQueryHandler : ICommandHandler<HistoryWeatherQuery, HistoryWeatherQueryResponse>
    {
        public Task<HistoryWeatherQueryResponse> Handle(HistoryWeatherQuery command)
        {
            throw new NotImplementedException();
        }
    }
}
