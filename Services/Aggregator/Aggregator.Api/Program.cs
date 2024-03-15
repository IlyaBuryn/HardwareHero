using Aggregator.Api;
using KafkaEventStream.Extensions;
using Microsoft.IdentityModel.Logging;
using static KafkaEventStream.Topics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomControllers();

builder.Services.AddFluentValidation();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AggregatorServiceInvokeHandler>(provider =>
{
    return new AggregatorServiceInvokeHandler(provider);
});
builder.Services.StartResponseWorker<AggregatorServiceInvokeHandler>(new ContributorTopics());

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

var app = builder.Build();

await app.DatabaseInitialization();
app.UseMiddleware<ExceptionHandlerMiddleware<BaseEntity>>();
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
