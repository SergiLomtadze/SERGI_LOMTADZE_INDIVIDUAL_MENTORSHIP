using ExadelMentorship.BackgroundApp;
using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.BusinessLogic.Services.MBus;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<MainProcess>();
        services.AddOptions<RabbitMQSettings>().BindConfiguration(nameof(RabbitMQSettings));
        services.AddSingleton<IConnection>(sp =>
        {
            return new ConnectionFactory
            {
                Uri = new Uri(sp.GetRequiredService<IOptions<RabbitMQSettings>>().Value.Uri)
            }.CreateConnection();
        });
        services.AddSingleton<IMessageConsumer, MessageBus>();
    })
    .Build();

await host.RunAsync();


