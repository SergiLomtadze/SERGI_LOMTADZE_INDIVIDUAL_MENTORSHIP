// See https://aka.ms/new-console-template for more information

using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.IntegrationTests;


var weather = DI.Resolve<Weather>();
var job = DI.Resolve<MainJob>();

await job.Execute(weather);



