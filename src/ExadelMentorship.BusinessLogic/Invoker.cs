using ExadelMentorship.BusinessLogic.Features;

using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ExadelMentorship.BusinessLogic
{
    public class Invoker
    {
        IRWOperation _rwOperation;
        IHttpClientFactory _httpClientFactory;

        ICommand command = null;

        public Invoker(IHttpClientFactory httpClientFactory, IRWOperation rwOperation)
        {
            _rwOperation = rwOperation;
            _httpClientFactory = httpClientFactory;
    }

        public ICommand GetCommand(int action)
        {
            if (action == 1)
                command = new CurrentWeather(_httpClientFactory, _rwOperation);

            //if (action == 2)
            //    command = new FutureWeather(_httpClientFactory, _rwOperation);

            if (action == 0)
                command = new ConsoleOperation();

            return command;
        }
    }
}
