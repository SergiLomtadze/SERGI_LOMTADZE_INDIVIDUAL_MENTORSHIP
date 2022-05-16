using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.IntegrationTests
{
    public class DIConfigurer
    {
        private ServiceProvider serviceProvider;
        public DIConfigurer()
        {
            serviceProvider = new ServiceCollection()
            .AddHttpClient()
            .BuildServiceProvider();
        }

        public IHttpClientFactory GetHttpClientFactory()
        {
            return serviceProvider.GetRequiredService<IHttpClientFactory>();
        }
    }
}
