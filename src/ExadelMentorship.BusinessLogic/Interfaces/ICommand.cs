using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Interfaces
{
    public interface ICommand
    {

    }

    public interface ICommandHandler<in T> where T : ICommand
    {
        Task Handle(T command);
    }
}
