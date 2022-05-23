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

        public Task Invoke(ICommand command) 
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<ICommand>>();
            return handler.Handle(command);
        }


    }
}
