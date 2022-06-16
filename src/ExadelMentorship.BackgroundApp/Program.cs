using ExadelMentorship.BackgroundApp;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<MainProcess>();
        services.AddOptions<RabbitMQSettings>().BindConfiguration(nameof(RabbitMQSettings));
    })
    .Build();

await host.RunAsync();


