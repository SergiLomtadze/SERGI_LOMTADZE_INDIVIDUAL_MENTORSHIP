using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic
{
    public class CommandInvoker
    {
        private readonly IServiceProvider _serviceProvider;
        public CommandInvoker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        private int GetActionFromUser()
        {
            string inputedLine;
            do
            {
                Console.WriteLine("Please enter Number \n" +
                    "1 - Current weather \n" +
                    "2 - Future weather \n" +
                    "0 - Close");
                inputedLine = Console.ReadLine();
            } while (!(inputedLine.Equals("0") || inputedLine.Equals("1") || inputedLine.Equals("2")));
            return Convert.ToInt32(inputedLine);
        }

        private Task Invoke(int command)
        {
            dynamic dto = ParseCommand(command);
            return Invoke(dto);
        }
        private Task Invoke<T>(T Command) where T : ICommand
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<T>>();
            return handler.Handle(Command);
        }
        private dynamic ParseCommand(int commnad)
        {
            if (commnad == 1)
            {
                return new CurrentWeatherCommand(_serviceProvider.GetRequiredService<IHttpClientFactory>());
            }
            if (commnad == 2)
            {
                return new FutureWeatherCommand(_serviceProvider.GetRequiredService<IHttpClientFactory>());
            }
            if (commnad == 0)
            {
                Environment.Exit(0);
            }
            throw
                new NotImplementedException();
        }
        public async Task Execute()
        {
            var command = this.GetActionFromUser();
            await this.Invoke(command);
            Console.ReadLine();
        }
    }
}
