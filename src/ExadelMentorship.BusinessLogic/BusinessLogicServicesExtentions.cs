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
using ExadelMentorship.BusinessLogic.Services.Weather;
using ExadelMentorship.DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace ExadelMentorship.BusinessLogic
{
    public static class BusinessLogicServicesExtentions
    {
        public static void AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<CommandInvoker>();
        
            services.AddSingleton<IWeatherApiService, WeatherApiService>();
            
            services.AddScoped<IReportServices, ReportServices>();

            services.AddSingleton<ICommandHandler<CurrentWeatherCommand, CurrentWeatherCommandResponse>, CurrentWeatherCommandHandler>();
        
            services.AddSingleton<ICommandHandler<FutureWeatherCommand, IEnumerable<City>>, FutureWeatherCommandHandler>();

            services.AddScoped<ICommandHandler<HistoryWeatherQuery, IEnumerable<HistoryWeatherQueryResponse>>, HistoryWeatherQueryHandler>();

            services.AddScoped<ICommandHandler<UserSubscriptionCommand, string>, UserSubscriptionCommandHandler>();

            services.AddScoped<ICommandHandler<UnSubscribeUserCommand, string>, UnSubscribeUserCommandHandler>();

            services.AddScoped<ICommandHandler<UserQuery, IEnumerable<ReportUser>>, UserQueryHandler>();            

            services.AddHttpClient();

            services.AddOptions<ForecastDaySettings>().BindConfiguration(nameof(ForecastDaySettings));

            services.AddOptions<MaxWeatherSettings>().BindConfiguration(nameof(MaxWeatherSettings));

            services.AddOptions<RabbitMQSettings>().BindConfiguration(nameof(RabbitMQSettings));

            AddMailServices(services);

            AddMessageBusServices(services);
        }

        public static void AddMailServices(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddOptions<SMTPConfig>().BindConfiguration(nameof(SMTPConfig));
        }

        public static void AddMessageBusServices(this IServiceCollection services)
        {
            services.AddSingleton<IConnection>(sp =>
                new ConnectionFactory
                {
                    Uri = new Uri(sp.GetRequiredService<IOptions<RabbitMQSettings>>().Value.Uri)
                }.CreateConnection()
            );
        }
    }
}
