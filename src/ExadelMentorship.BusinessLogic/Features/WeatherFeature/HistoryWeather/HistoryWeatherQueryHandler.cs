using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.HistoryWeather
{
    public class HistoryWeatherQueryHandler : ICommandHandler<HistoryWeatherQuery, HistoryWeatherQueryResponse>
    {
        private IWeatherHistoryRepository _weatherHistoryRepository;
        public HistoryWeatherQueryHandler(IWeatherHistoryRepository weatherHistoryRepository)
        {
            _weatherHistoryRepository = weatherHistoryRepository;
        }

        public async Task<HistoryWeatherQueryResponse> Handle(HistoryWeatherQuery query)
        {
            var list = _weatherHistoryRepository.GetAll();
            if (list.Any() && query.From > DateTimeOffset.MinValue && query.To > DateTimeOffset.MinValue)
            {
                list = list.Where(b =>
                    b.Time.Date >= query.From.Date &&
                    b.Time.Date <= query.To.Date);
            }
            
            //will be removed in next PR, added just to have fake await inside Task             
            await Task.Delay(1);

            return new HistoryWeatherQueryResponse
            {
                //real await will be here in next PR
            };
        }
    }
}
