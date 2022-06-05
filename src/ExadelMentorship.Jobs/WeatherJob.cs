using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.DataAccess;
using Hangfire;
using Microsoft.Extensions.Options;


namespace ExadelMentorship.WebApi.Jobs
{
    public class WeatherJob : IWeatherJob
    {
        public void HistorySaving()
        {
            throw new NotImplementedException();
        }
    }
}
