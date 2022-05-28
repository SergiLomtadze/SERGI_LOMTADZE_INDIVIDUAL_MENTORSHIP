using ExadelMentorship.BusinessLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic
{
    public class CommandInvoker2
    {
        private readonly IServiceProvider _serviceProvider;
        public CommandInvoker2(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResult> Invoke<TResult>(ICommand2<TResult> command)
        {
            return InvokeCore((dynamic)command, default(TResult));
        }

        private Task<TResult> InvokeCore<TCommand, TResult>(TCommand command, TResult result) where TCommand : ICommand2<TResult>
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler2<TCommand, TResult>>();
            return handler.Handle(command);
        }

    }
}
