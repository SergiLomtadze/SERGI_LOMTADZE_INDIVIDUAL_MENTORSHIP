using ExadelMentorship.Auth;

var builder = WebApplication.CreateBuilder(args);

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
