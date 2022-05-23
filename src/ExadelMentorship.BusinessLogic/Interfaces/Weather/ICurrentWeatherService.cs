using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces
{
    public interface ICurrentWeatherService
    {
        public Task<double> GetTemperatureByCityName(string name);
    }
}
