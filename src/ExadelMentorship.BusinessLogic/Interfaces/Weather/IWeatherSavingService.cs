using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces.Weather
{
    public interface IWeatherSavingService
    {
        Task SaveInDb(string cityName);
    }
}
