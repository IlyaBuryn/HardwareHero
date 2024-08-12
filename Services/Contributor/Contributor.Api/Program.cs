using KafkaEventStream.Topics;
using Microsoft.IdentityModel.Logging;
using HardwareHero.Shared.Extensions;
using KafkaEventStream.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureSecretsFile();

builder.Services.AddFluentValidation();
builder.Services.ConfigureOpenTelemetry(builder);

builder.Services.ConfigureCommonJwtAuthentication(builder);
builder.Services.ConfigurePolicyAuthorization();

var connectionString = builder.Configuration.GetConnectionString(ConnectionNames.ContributorsConnection);
builder.Services.ConfigureBusinessLogicLayer(connectionString);
builder.Services.ConfigureOptions<PageSizeOptions>(builder.Configuration);
builder.Services.ConfigureOptions<ImagesSaveOptions>(builder.Configuration);
builder.Services.ConfigureKafkaRequestsBackgroundWorker<ContributorTopics>();

builder.Services.AddCustomControllers();

builder.Services.ConfigureSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureCORSPolicy();

IdentityModelEventSource.ShowPII = true;
builder.Host.ConfigureElasticLogging();
var app = builder.Build();

await app.DatabaseInitialization();
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