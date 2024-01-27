using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.Options;
using Configurator.BusinessLogic.Extensions;
using Configurator.Api.Extensions;
using HardwareHero.Services.Shared.Middlewares;
using HardwareHero.Services.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterMongoClassMap();

builder.Services.AddCustomControllers();

builder.Services.AddFluentValidation();

builder.Services.ConfigureOptions<DatabaseOptions>(
    builder.Configuration, 
    ConnectionNames.ConfiguratorConnection);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.ConfigureBusinessLogicLayer();

builder.Services.AddIdentityServerAuthentication();

builder.Services.AddApiScopeAuthorization();

builder.Services.ConfigureCustomServices();

builder.Services.AddCors();

var app = builder.Build();

await app.ConfigureDatabaseAsync();

app.UseMiddleware<ExceptionHandlerMiddleware<BaseEntity>>();

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
