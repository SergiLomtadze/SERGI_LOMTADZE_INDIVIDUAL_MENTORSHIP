using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic
{
    public class MainJob
    {
        IRWOperation _rwOperation;
        ICurrentWeather _weather;
        IHttpClientFactory _httpClientFactory;
        public MainJob(IRWOperation rwOperation, ICurrentWeather weather, IHttpClientFactory httpClientFactory)
        {
            _rwOperation = rwOperation;
            _weather = weather;
            _httpClientFactory = httpClientFactory; 
        }
       
        private int GetActionFromUser()
        {
            string inputedLine;
            do
            {
                _rwOperation.WriteLine("Please enter Number \n" +
                    "1 - Current weather \n" +
                    "2 - Future weather \n" +
                    "0 - Close");
                inputedLine = _rwOperation.ReadLine();
            } while (!(inputedLine.Equals("0")||inputedLine.Equals("1")||inputedLine.Equals("2")));
            return Convert.ToInt32(inputedLine);
        }

        public async Task Do()
        {
            var action = this.GetActionFromUser();

            Invoker invoker = new Invoker(_httpClientFactory,_rwOperation);

            ICommand command = invoker.GetCommand(action);
            await command.Execute();

            _rwOperation.ReadLine();
        }
    }
}
