using Aggregator.Api;
using KafkaEventStream.Topics;
using Microsoft.IdentityModel.Logging;
using HardwareHero.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomControllers();

builder.Services.AddFluentValidation();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.StartKafkaMediator<ContributorTopics, AggregatorEndpointManager>();

var connectionString = builder.Configuration.GetConnectionString(ConnectionNames.AggregatorConnection);
if (connectionString != null)
{
    builder.Services.ConfigureBusinessLogicLayer(connectionString);
}

builder.Services.ConfigureOptions<PageSizeOptions>(builder.Configuration);
builder.Services.ConfigureOptions<ImagesSaveOptions>(builder.Configuration);

builder.Services.AddIdentityServerAuthentication();

builder.Services.AddApiScopeAuthorization();
builder.Services.AddCors();

IdentityModelEventSource.ShowPII = true;

builder.Host.ConfigureElasticLogging();

var app = builder.Build();

await app.DatabaseInitialization();
app.UseMyCustomMiddlewares();
app.UseHttpsRedirection();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization("ApiScope");

app.Run();
