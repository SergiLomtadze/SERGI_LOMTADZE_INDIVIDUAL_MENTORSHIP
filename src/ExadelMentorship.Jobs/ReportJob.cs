using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Services.Mail;
using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ExadelMentorship.Jobs
{
    public class ReportJob : BackgroundService
    {
        private IReportUserRepo _reportUserRepo;
        private IReportServices _reportServices;
        private readonly IHttpClientFactory _httpClientFactory;
        private ApiConfig _apiConfig;

        public ReportJob(IReportUserRepo reportUserRepo, IReportServices reportServices,
            IHttpClientFactory httpClientFactory, IOptions<ApiConfig> apiConfig)
        {
            _reportUserRepo = reportUserRepo;
            _reportServices = reportServices;
            _httpClientFactory = httpClientFactory;
            _apiConfig = apiConfig.Value;
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
        public async Task Execute(ReportUser reportUser)
        {
            var report = await _reportServices.GenerateReportForUser(reportUser.Id);
            Message message = new Message(reportUser.Email, reportUser.UserName, report);

            var url = _apiConfig.publishMessage;
            var httpClient = _httpClientFactory.CreateClient();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, Application.Json);

            await httpClient.PostAsync(url, httpContent);
        }
    }
}
