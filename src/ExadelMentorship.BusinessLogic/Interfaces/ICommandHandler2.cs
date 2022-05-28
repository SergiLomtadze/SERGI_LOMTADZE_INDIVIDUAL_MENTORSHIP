using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces
{
    public interface ICommandHandler2<in TCommand, TResult> where TCommand : ICommand2<TResult>
    {
        Task<TResult> Handle(TCommand command);
    }
}
