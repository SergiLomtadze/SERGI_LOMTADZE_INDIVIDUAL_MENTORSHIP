using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.DataAccess;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.Reports.UserUnSubscription
{
    public class UnSubscribeUserCommandHandler : ICommandHandler<UnSubscribeUserCommand, string>
    {
        private IReportUserRepo _reportUserRepo;
        public UnSubscribeUserCommandHandler(IReportUserRepo reportUserRepo)
        {
            _reportUserRepo = reportUserRepo;
        }

        public async Task<string> Handle(UnSubscribeUserCommand command)
        {
            _reportUserRepo.Delete(command.UserId);
            return await Task.FromResult($"User with Id: {command.UserId} succesfully unsubscribed");
        }
    }
}
