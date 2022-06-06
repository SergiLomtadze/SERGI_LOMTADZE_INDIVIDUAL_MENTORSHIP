using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature.HistoryWeather
{
    public class HistoryWeatherQueryHandler : ICommandHandler<HistoryWeatherQuery, IEnumerable<HistoryWeatherQueryResponse>>
    {
        private IWeatherHistoryRepository _weatherHistoryRepository;
        public HistoryWeatherQueryHandler(IWeatherHistoryRepository weatherHistoryRepository, IServiceProvider services)
        {
            _weatherHistoryRepository = weatherHistoryRepository;
        }

        public async Task<IEnumerable<HistoryWeatherQueryResponse>> Handle(HistoryWeatherQuery query)
        {
            var list = _weatherHistoryRepository.GetByCity(query.CityName);

            if (list.Any() && query.From > DateTime.MinValue && query.To > DateTime.MinValue)
            {
                list = list.Where(b =>
                    b.Time.Date >= query.From.Date &&
                    b.Time.Date <= query.To.Date);
            }

            var response = new List<HistoryWeatherQueryResponse>();
            foreach (var item in list)
            {
                response.Add(new HistoryWeatherQueryResponse
                {
                    CityName = item.CityName,
                    Temperature = item.Temperature,
                    Time = item.Time,
                });
            }

            return await Task.FromResult(response);
        }
    }
}
