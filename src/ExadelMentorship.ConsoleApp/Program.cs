// See https://aka.ms/new-console-template for more information

using ExadelMentorship.ConsoleApp;

var job = DI.Resolve<MainJob>();

await job.Execute();



