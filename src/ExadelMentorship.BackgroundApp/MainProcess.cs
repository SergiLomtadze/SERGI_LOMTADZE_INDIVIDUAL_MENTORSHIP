namespace ExadelMentorship.BackgroundApp
{
    public class MainProcess : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Backgorund app working");
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}