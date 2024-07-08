using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using JWTAuthManager;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json",optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCustomJwtAuthentication();
builder.Services.AddHealthChecks();

var app = builder.Build();
await app.UseOcelot();
app.MapHealthChecks("/healthx");
app.UseAuthentication();
app.UseAuthorization();

app.Run();
