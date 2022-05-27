using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
    {
        Task<TResult> Handle(TCommand command);
    }
}
