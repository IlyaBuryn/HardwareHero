using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.Settings;
using Configurator.BusinessLogic.Extensions;
using Configurator.Api.Extensions;
using HardwareHero.Services.Shared.Middlewares;
using Configurator.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomControllers();

builder.Services.AddFluentValidation();

builder.Services.ConfigureOptions<DatabaseSettings>(
    builder.Configuration, 
    ConnectionNames.ConfiguratorConnection);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.ConfigureBusinessLogicLayer();

builder.Services.AddIdentityServerAuthentication();

builder.Services.AddApiScopeAuthorization();

builder.Services.ConfigureDatabaseOptions();

builder.Services.AddCors();

var app = builder.Build();

var config = app.Services.GetService<Config>();
await config!.SeedDatabaseAsync();

app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers().RequireAuthorization("ApiScope");

app.Run();
