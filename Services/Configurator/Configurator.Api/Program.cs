using HardwareHero.Shared.Extensions;
using HardwareHero.Shared.Extensions.Elastic;
using HardwareHero.Shared.Extensions.Secrets;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureAuthenticationSecretsFiles()
    .ConfigureElasticLogging(builder.Configuration);

builder.Services
    .ConfigureBusinessLayer()
    //.ConfigureEventServices(builder)
    //.ConfigureEventHandlers()
    .ConfigureOptions(builder);

builder.Services
    .ConfigureFluentValidation()
    .ConfigureOpenTelemetry(builder)
    .ConfigureCommonJwtAuthentication(builder)
    .ConfigurePolicyAuthorization()
    .ConfigureSwagger()
    .ConfigureCORSPolicy()
    .ConfigureCustomControllers();

builder.Services
    .AddEndpointsApiExplorer();

IdentityModelEventSource.ShowPII = true;
var app = builder.Build();

await app.SetupDefaultDataAsync();

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
