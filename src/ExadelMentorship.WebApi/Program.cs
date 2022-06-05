using ExadelMentorship.BusinessLogic;
using ExadelMentorship.Persistence;
using ExadelMentorship.WebApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices();
builder.Services.AddBusinessLogicServices();
builder.Services.AddJobServices();


builder.Configuration.AddJsonFile("appsettings.local.json");

var app = builder.Build();

app.UseExceptionHandler(ExceptionHandler.GetExceptionHandlerOptions());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
