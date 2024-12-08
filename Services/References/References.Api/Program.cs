using HardwareHero.Shared.Extensions;
using HardwareHero.Shared.Extensions.Secrets;
using HardwareHero.Shared.Extensions.Elastic;
using HardwareHero.Shared.Extensions.Monitoring;
using Microsoft.IdentityModel.Logging;
using References.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureAuthenticationSecretsFiles()
    .ConfigureElasticLogging(builder.Configuration);

builder.Services
    .ConfigureBusinessLayer()
    .ConfigureEventServices(builder)
    .ConfigureEventHandlers()
    .ConfigureOptions(builder);


builder.Services
    .ConfigureFluentValidation()
    .ConfigureOpenTelemetry(builder.Configuration)
    .ConfigureCommonJwtAuthentication(builder)
    .ConfigurePolicyAuthorization()
    .ConfigureSwagger()
    .ConfigureCORSPolicy()
    .ConfigureCustomControllers();

builder.Services
    .AddEndpointsApiExplorer();

await builder.Services
    .ConfigureSeedDatabaseAsync();

IdentityModelEventSource.ShowPII = true;
var app = builder.Build();

app.UseCommonCustomMiddlewares();

app.UseCors("default");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization("ApiScope");

app.Run();
