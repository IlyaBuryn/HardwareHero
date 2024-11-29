using Storage.Api.Extensions;
using HardwareHero.Shared.Extensions;
using Storage.BusinessLogic.Extensions;
using Microsoft.IdentityModel.Logging;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureSecretsFile()
    .ConfigureElasticLogging();

builder.Services
    .ConfigureBusinessLayer()
    .ConfigureEventServices(builder)
    .ConfigureEventHandlers()
    .ConfigureOptions(builder);

builder.Services
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

var storagePath = builder.Configuration["Storage:BasePath"] ?? "/data/files";
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(storagePath),
    RequestPath = "/files",
    ServeUnknownFileTypes = true,
    DefaultContentType = "application/octet-stream"
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
