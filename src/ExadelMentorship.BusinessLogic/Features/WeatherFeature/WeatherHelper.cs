using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public static class WeatherHelper
    {
        public static string GetCommentByTemperature(double temperature)
        {
            if (temperature > 30)
            {
                return "It's time to go to the beach";
            }
            else if (temperature > 20)
            {
                return "Good weather";
            }
            else if (temperature > 0)
            {
                return "It's fresh";
            }
            else
            {
                return "Dress warmly";
            }
        }
    }
}
