using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Interfaces.MessageBus;
using ExadelMentorship.BusinessLogic.Services.MBus;
using ExadelMentorship.Persistence;
using ExadelMentorship.WebApi;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
builder.Services.AddSingleton<IMessageProducer, MessageBus>();
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
    {
        o.Authority = builder.Configuration.GetSection("AuthConfig")["authority"];
        o.Audience = "WebApi";
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "mailApi");
    });
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
