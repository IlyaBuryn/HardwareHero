using Gateway.Ocelot.Extensions;
using HardwareHero.Shared.Extensions;
using HardwareHero.Shared.Extensions.Secrets;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

// TODO: Add the ability to view the entire configuration
var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureAuthenticationSecretsFiles();

builder
    .ConfigureOcelotFiles();

builder.Services
    .AddOcelot(builder.Configuration);

builder.Services
    .ConfigureEventServices(builder)
    .ConfigureCORSPolicy();

var app = builder.Build();

app.UseCommonCustomMiddlewares();

app.UseCors("default");

app.UseStaticFiles();
app.UseRouting();

app.UseRefreshTokenMiddleware();

app.UseHttpsRedirection();

await app.UseOcelot();

app.Run();
