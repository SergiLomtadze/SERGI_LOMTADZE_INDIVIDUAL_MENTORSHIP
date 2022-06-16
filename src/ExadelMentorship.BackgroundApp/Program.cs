using ExadelMentorship.BackgroundApp;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<MainProcess>();
    })
    .Build();

await host.RunAsync();
