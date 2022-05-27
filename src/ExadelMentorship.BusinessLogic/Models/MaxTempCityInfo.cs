using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Models
{
    public class MaxTempCityInfo
    {
        public string Name { get; set; }
        public long DurationTime { get; set; }
        public double Temperature { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsCancelled { get; set; }        
    }
}
