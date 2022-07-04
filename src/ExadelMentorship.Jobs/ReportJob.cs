using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExadelMentorship.Jobs
{
    public class ReportJob : BackgroundService
    {
        private readonly IMessageProducer _messagePublisher;
        private IReportUserRepo _reportUserRepo;
        private IReportServices _reportServices;

        public ReportJob(IMessageProducer messagePublisher, IReportUserRepo reportUserRepo, IReportServices reportServices)
        {
            _messagePublisher = messagePublisher;
            _reportUserRepo = reportUserRepo;
            _reportServices = reportServices;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var all = await _reportUserRepo.GetAll();
            foreach (var item in all)
            {
                RecurringJob.AddOrUpdate(
                    $"ReportSendingJobFor_{item.UserName}",
                    () => this.Execute(item),
                    $"0 * {item.Period} * * ?"
                );
            }
        }
        
        //System.NotSupportedException: 'Only public methods can be invoked in the background'
        public void Execute(ReportUser reportUser)
        {
            var report = _reportServices.GenerateReportForUser(reportUser.Id);
            var message = new { Email = reportUser.Email, UserName = reportUser.UserName, Report = report };

            _messagePublisher.SendMessage(message, "sendMail");
        }
    }
}
