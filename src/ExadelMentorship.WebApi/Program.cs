using ExadelMentorship.BusinessLogic;
using ExadelMentorship.Persistence;
using ExadelMentorship.WebApi;
using ExadelMentorship.WebApi.Jobs;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
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
builder.Services.AddPersistenceServices();
builder.Services.AddBusinessLogicServices();
builder.Services.AddJobServices();
builder.Services.AddHostedService<WeatherJob>();
builder.Configuration.AddJsonFile("appsettings.local.json");

builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("Oidc", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OpenIdConnect,
        OpenIdConnectUrl = new Uri(builder.Configuration.GetSection("AuthConfig")["url"]),
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Id = "Oidc", Type = ReferenceType.SecurityScheme }
            },
            new List<string> { "WebApi.read", "role" }
        }
    });
});
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
    {
        o.Authority = builder.Configuration.GetSection("AuthConfig")["authority"];
        o.Audience = "WebApi";
    });

WebApplication app = builder.Build();

app.UseExceptionHandler(ExceptionHandler.GetExceptionHandlerOptions());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.OAuthClientId("api-swagger");
        o.OAuthScopes("WebApi.read", "role");
        o.OAuthScopeSeparator(" ");
        o.OAuthUsePkce();
    });
}

app.UseHttpsRedirection()
    .UseDefaultFiles("/swagger")
    .UseAuthentication()
    .UseAuthorization();

app.UseHangfireDashboard("/dashboard");
app.MapControllers();
app.Run();
