using ExadelMentorship.BusinessLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            return InvokeCommand((dynamic)command);
        }

        private Task InvokeCommand<T>(T command) where T : ICommand
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<T>>();
            return handler.Handle(command);
        }

    }
}
