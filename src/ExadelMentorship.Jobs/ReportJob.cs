using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.BusinessLogic.Services.Mail;
using ExadelMentorship.DataAccess;
using ExadelMentorship.DataAccess.Entities;
using Hangfire;
using IdentityModel.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
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
        private ClientInfo _clientInfo;
        private AuthConfig _authConfig;

        public ReportJob(IReportUserRepo reportUserRepo, IReportServices reportServices,
            IHttpClientFactory httpClientFactory, IOptions<ApiConfig> apiConfig, 
            IOptions<ClientInfo> clientInfo, IOptions<AuthConfig> authConfig)
        {
            _reportUserRepo = reportUserRepo;
            _reportServices = reportServices;
            _httpClientFactory = httpClientFactory;
            _apiConfig = apiConfig.Value;
            _clientInfo = clientInfo.Value;
            _authConfig = authConfig.Value;
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
            var client = _httpClientFactory.CreateClient();
            var tokenInfo = await client.GetDiscoveryDocumentAsync(_authConfig.Authority);
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = tokenInfo.TokenEndpoint,
                ClientId = "reportJob",
                ClientSecret = _clientInfo.ReportJobSecret,
                Scope = "mailApi"
            });

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var report = await _reportServices.GenerateReportForUser(reportUser.Id);
            Message message = new Message(reportUser.Email, reportUser.UserName, report);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, Application.Json);

            var url = _apiConfig.publishMessage;

            await apiClient.PostAsync(url, httpContent);
        }
    }
}
