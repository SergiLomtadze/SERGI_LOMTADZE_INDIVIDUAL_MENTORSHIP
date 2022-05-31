using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature.MaxWeather;
using ExadelMentorship.BusinessLogic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExadelMentorship.ConsoleApp
{
    public class DI
    {
        public static T Resolve<T>()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            ServiceCollection service = (ServiceCollection)new ServiceCollection()
                .AddSingleton(config)
                .AddSingleton<MainJob>()
                .AddSingleton<IRWOperation, ConsoleOperation>()
                .AddSingleton<ICommandHandler<MaxWeatherCommand, MaxWeatherCommandResponse>, MaxWeatherCommandHandler>();

            service.AddBlServices();

            return service.BuildServiceProvider().GetRequiredService<T>();
        }
    }
}
