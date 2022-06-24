using ExadelMentorship.BackgroundApp;
using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.BusinessLogic.Services.MBus;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<MainProcess>();
        services.AddOptions<RabbitMQSettings>().BindConfiguration(nameof(RabbitMQSettings));
        services.AddSingleton<IConnectionProvider,ConnectionProvider>();
        services.AddSingleton<IMessageConsumer, MessageBus>();
    })
    .Build();

await host.RunAsync();


