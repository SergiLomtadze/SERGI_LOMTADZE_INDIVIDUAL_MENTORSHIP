// See https://aka.ms/new-console-template for more information
using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.IntegrationTests;
using Microsoft.Extensions.DependencyInjection;
using System;

var weather = DI.Resolve<Weather>();

await consoleJob.DoJob(weather);


