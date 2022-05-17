using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.IntegrationTests
{
    public class DI
    {
        public static T Resolve<T>()
        {
            return new ServiceCollection()
            .AddSingleton<Weather>()
            .AddHttpClient()
            .BuildServiceProvider()
            .GetRequiredService<T>();
        }
    }
}
