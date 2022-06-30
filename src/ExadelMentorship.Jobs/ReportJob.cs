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
        private IServiceProvider _services;

        public ReportJob(IMessageProducer messagePublisher,IServiceProvider services)
        {
            _messagePublisher = messagePublisher;
            _services = services;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _services.CreateScope();
            var all = await scope.ServiceProvider.GetRequiredService<IReportUserRepo>().GetAll();
            foreach (var item in all)
            {
                RecurringJob.AddOrUpdate(
                    $"ReportSendingJobFor_{item.UserName}",
                    () => this.Execute(item),
                    $"0 * {item.Period} * * ?"
                );
            }
        }
        public async Task Execute(ReportUser reportUser)
        {
            using var scope = _services.CreateScope();
            var report = scope.ServiceProvider.GetRequiredService<IReportServices>().GenerateReportForUser(reportUser.Id);
            var message = new { Email = reportUser.Email, UserName = reportUser.UserName, Report = report };

            await _messagePublisher.SendMessage(message, "sendMail");
        }
    }
}
