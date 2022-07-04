using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Services
{
    public class ReportServices : IReportServices
    {
        private IReportUserRepo _reportUserRepo;
        private IWeatherHistoryRepository _weatherHistoryRepository;
        public ReportServices(IReportUserRepo reportUserRepo, IWeatherHistoryRepository weatherHistoryRepository)
        {
            _reportUserRepo = reportUserRepo;
            _weatherHistoryRepository = weatherHistoryRepository;
        }
        public async Task<string> GenerateReportForUser(int userId)
        {
            string report = $"The report was generated at: {DateTime.Now}. \n";
            var userInfo= await _reportUserRepo.GetById(userId);
            var period = userInfo.Period;
            var cities = userInfo.Cities.Split(',');

            foreach (var city in cities)
            {
                try
                {
                    var averageTemperature = _weatherHistoryRepository.GetByCity(city).AsEnumerable()
                        .Where(x => x.Time > DateTime.Now.AddHours(-period))
                        .Select(x => x.Temperature).Average();
                    report += $"{city} average temperature: {averageTemperature} C. \n";
                }
                catch (Exception)
                {
                    report += $"{city} no statistics \n";
                }
            }

            return report;
        }
    }
}
