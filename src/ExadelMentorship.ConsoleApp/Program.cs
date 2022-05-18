// See https://aka.ms/new-console-template for more information

using ExadelMentorship.BusinessLogic;
using ExadelMentorship.IntegrationTests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using IHost host = Host.CreateDefaultBuilder(args).Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

var min = config["MinForecastDay"];
var max = config["MaxForecastDay"];


var job = DI.Resolve<MainJob>();

await job.Do();





