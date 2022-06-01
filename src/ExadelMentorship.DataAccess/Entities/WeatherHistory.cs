using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.DataAccess.Entities
{
    public class WeatherHistory
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public double Temperature { get; set; }
        public DateTime Time { get; set; }
    }
}
