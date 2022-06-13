using ExadelMentorship.Auth;
using ExadelMentorship.Auth.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions<IdentityConfig>().BindConfiguration(nameof(IdentityConfig));

builder.Services.AddIdentityServer(o =>
    {
    })
    .AddDeveloperSigningCredential()
    .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
    .AddInMemoryApiScopes(IdentityConfiguration.Scopes)
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddTestUsers(IdentityConfiguration.TestUsers.ToList());


builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseHttpsRedirection()
    .UseStaticFiles()
    .UseDefaultFiles()
    .UseRouting()
    .UseIdentityServer()
    .UseAuthorization()
    .UseEndpoints(e => e.MapDefaultControllerRoute());

app.Run();
