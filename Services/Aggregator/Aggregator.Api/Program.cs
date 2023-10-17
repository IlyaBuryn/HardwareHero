using Aggregator.Api.Extensions;
using Aggregator.BusinessLogic.Extensions;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.Middlewares;
using HardwareHero.Services.Shared.Options;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomControllers();

builder.Services.AddFluentValidation();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString(ConnectionNames.AggregatorConnection);
if (connectionString != null)
{
    builder.Services.ConfigureBusinessLogicLayer(connectionString);
}

builder.Services.ConfigureOptions<PageSizeOptions>(builder.Configuration);
builder.Services.ConfigureOptions<DataSaveOptions>(builder.Configuration);

builder.Services.AddIdentityServerAuthentication();

builder.Services.AddApiScopeAuthorization();

builder.Services.AddCors();

IdentityModelEventSource.ShowPII = true;

var app = builder.Build();

await app.DatabaseInitialization();
app.UseMiddleware<ExceptionHandlerMiddleware>();
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
