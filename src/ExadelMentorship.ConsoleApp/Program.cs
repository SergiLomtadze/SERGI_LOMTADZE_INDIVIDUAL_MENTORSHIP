// See https://aka.ms/new-console-template for more information

using ExadelMentorship.BusinessLogic;
using ExadelMentorship.IntegrationTests;

var job = DI.Resolve<MainJob>();

await job.Execute();



