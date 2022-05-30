using ExadelMentorship.BusinessLogic;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBlServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseExceptionHandler(new ExceptionHandlerOptions
{
    ExceptionHandler = (c) =>
    {
        var exception = c.Features.Get<IExceptionHandlerFeature>();
        var statusCode = exception.Error.GetType().Name switch
        {
            "NotFoundException" => StatusCodes.Status404NotFound,
            "FormatException" => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        c.Response.StatusCode = statusCode;
        c.Response.WriteAsync(exception.Error.Message);

        return Task.CompletedTask;
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
