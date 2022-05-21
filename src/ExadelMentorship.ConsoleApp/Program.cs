// See https://aka.ms/new-console-template for more information

using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.IntegrationTests;
using Microsoft.Extensions.Configuration;
using System;

var job = DI.Resolve<CommandInvoker>();

try
{
    await job.Execute();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.ReadKey();
}




