using Ocelot.DependencyInjection;
using HardwareHero.Shared.Extensions;
using Ocelot.Middleware;
using ApiGateway.Ocelot.Extensions;
using KafkaEventStream.Topics;
using KafkaEventStream.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureSecretsFile();

builder.ConfigureOcelotFile();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.ConfigureKafkaRequestsBackgroundWorker<IdentityTopics>();
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