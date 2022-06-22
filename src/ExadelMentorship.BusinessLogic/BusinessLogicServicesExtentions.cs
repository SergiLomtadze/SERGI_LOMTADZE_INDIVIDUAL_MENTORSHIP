using ExadelMentorship.BusinessLogic.Features.Reports.UserQuery;
using ExadelMentorship.BusinessLogic.Features.Reports.UserSubscription;
using ExadelMentorship.BusinessLogic.Features.Reports.UserUnSubscription;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.CurrentWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.HistoryWeather;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.BusinessLogic.Services;
using ExadelMentorship.BusinessLogic.Services.Mail;
using ExadelMentorship.DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ExadelMentorship.BusinessLogic
{
    public static class BusinessLogicServicesExtentions
    {
        public static void AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<CommandInvoker>();
        
            services.AddSingleton<IWeatherApiService, WeatherApiService>();
        
            services.AddSingleton<ICommandHandler<CurrentWeatherCommand, CurrentWeatherCommandResponse>, CurrentWeatherCommandHandler>();
        
            services.AddSingleton<ICommandHandler<FutureWeatherCommand, IEnumerable<City>>, FutureWeatherCommandHandler>();

            services.AddScoped<ICommandHandler<HistoryWeatherQuery, IEnumerable<HistoryWeatherQueryResponse>>, HistoryWeatherQueryHandler>();

            services.AddScoped<ICommandHandler<UserSubscriptionCommand, string>, UserSubscriptionCommandHandler>();

            services.AddScoped<ICommandHandler<UnSubscribeUserCommand, string>, UnSubscribeUserCommandHandler>();

            services.AddScoped<ICommandHandler<UserQuery, IEnumerable<ReportUser>>, UserQueryHandler>();
            
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddHttpClient();

            services.AddOptions<ForecastDaySettings>().BindConfiguration(nameof(ForecastDaySettings));

            services.AddOptions<MaxWeatherSettings>().BindConfiguration(nameof(MaxWeatherSettings));

            services.AddOptions<RabbitMQSettings>().BindConfiguration(nameof(RabbitMQSettings));
            
            services.AddOptions<SMTPConfig>().BindConfiguration(nameof(SMTPConfig));
            
        }
    }
}
