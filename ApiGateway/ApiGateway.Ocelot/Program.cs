using Ocelot.DependencyInjection;
using HardwareHero.Shared.Extensions;
using Ocelot.Middleware;
using ApiGateway.Ocelot.Extensions;
using HardwareHero.Shared.Extensions.Secrets;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureAuthenticationSecretsFiles();

builder.ConfigureOcelotFile();
builder.Services.AddOcelot(builder.Configuration);
// TODO: change later
//builder.Services.ConfigureKafkaRequestsBackgroundWorker<IdentityTopics>();
builder.Services.ConfigureCORSPolicy();

var app = builder.Build();

app.UseCommonCustomMiddlewares();

app.UseCors("default");

app.UseStaticFiles();
app.UseRouting();

app.UseTokenRefreshMiddleware();

app.UseHttpsRedirection();

await app.UseOcelot();


app.Run();