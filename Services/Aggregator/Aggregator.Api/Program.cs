using Microsoft.IdentityModel.Logging;
using HardwareHero.Shared.Extensions;
using EventDriven.Kafka.Config;
using EventDriven.Kafka.Extensions;
using Microsoft.AspNetCore.Mvc;
using EventDriven.Shared.Services;
using Mail.DTOs.Events;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureSecretsFile();

//var kafkaConfig = builder.Configuration.GetSection("KafkaConfig").Get<KafkaConfig>();
//builder.Services.AddKafkaProducer<MailSettingsEvent>(kafkaConfig);

builder.Services.AddFluentValidation();
builder.Services.ConfigureOpenTelemetry(builder);

builder.Services.ConfigureCommonJwtAuthentication(builder);
builder.Services.ConfigurePolicyAuthorization();

var connectionString = builder.Configuration.GetConnectionString(ConnectionNames.AggregatorConnection);
builder.Services.ConfigureBusinessLogicLayer(connectionString ?? "");
builder.Services.ConfigureOptions<PageSizeOptions>(builder.Configuration);

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

//app.MapGet("/event-producing", async ([FromServices] IProducerService<MailSettingsEvent> producer, CancellationToken cancellationToken) =>
//{
//    await producer.ProduceAsync(new MailSettingsEvent
//    {
//        Timestamp = DateTime.UtcNow - TimeSpan.FromDays(10000),
//        Username = "Test user",
//        RecipientMailAddress = "ilya.buryn@gmail.com"
//    }, cancellationToken);

//    return "Event Send!";
//});

//app.MapGet("/message-request", async ([FromServices] IRequestService<TestRequest, TestReply> producer, CancellationToken cancellationToken) =>
//{
//    Console.WriteLine("-----> Starting request...");
//    var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
//    var response = await producer.SendRequestAsync(new TestRequest(), cts.Token);
//    Console.WriteLine("<----- Reply received.");

//    return Results.Ok(response);
//});

app.Run();