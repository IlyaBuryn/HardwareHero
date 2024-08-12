using Identity.Api;
using Identity.Api.Data;
using Identity.Api.Extensions;
using KafkaEventStream.Extensions;
using KafkaEventStream.Topics;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureSecretsFile();

builder.Services.ConfigureFluentValidation();
builder.Services.ConfigureOpenTelemetry(builder);

builder.Services.ConfigureSQLServerContexts(builder);
builder.Services.ConfigureCommonJwtAuthentication(builder);
builder.Services.ConfigurePolicyAuthorization();
builder.Services.ConfigureCustomIdentity();

builder.Services.ConfigureServices();
builder.Services.ConfigureBackgroundServices();
builder.Services.ConfigureKafkaMediatorBackgroundWorker<IdentityTopics, IdentityEndpointManager>();
builder.Services.ConfigureKafkaRequestsBackgroundWorker<MailTopics>();

builder.Services.AddCustomControllers();

builder.Services.ConfigureSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureCORSPolicy();

builder.Host.ConfigureElasticLogging();
var app = builder.Build();

app.UseMigration<GrantsDbContext>();
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

await app.UseDatabaseSeeding(args);

app.Run();
