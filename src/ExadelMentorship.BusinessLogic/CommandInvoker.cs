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

        public Task<TResult> Invoke<TResult>(ICommand<TResult> command)
        {
            return InvokeCore((dynamic)command, default(TResult));
        }

        private Task<TResult> InvokeCore<TCommand, TResult>(TCommand command, TResult result) where TCommand : ICommand<TResult>
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            return handler.Handle(command);
        }

    }
}
