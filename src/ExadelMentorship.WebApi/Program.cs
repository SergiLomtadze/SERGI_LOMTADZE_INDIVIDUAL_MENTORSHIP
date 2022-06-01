using ExadelMentorship.BusinessLogic;
using ExadelMentorship.Persistence;
using ExadelMentorship.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBlServices();
builder.Services.AddPersistence();

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
