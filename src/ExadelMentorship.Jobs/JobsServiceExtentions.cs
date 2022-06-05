using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.WebApi.Jobs;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExadelMentorship.Persistence
{
    public static class JobsServiceExtentions
    {
        public static void AddJobServices(this IServiceCollection services)
        {
            services.AddHangfire((s, option) =>
                option.UseSqlServerStorage(s.GetRequiredService<IConfiguration>().GetConnectionString("HangfireConnection")));
            services.AddHangfireServer();
            services.AddOptions<HistorySettingStorage>().BindConfiguration(nameof(HistorySettingStorage));
            services.AddScoped<IWeatherJob, WeatherJob>();
        }
    }
}
