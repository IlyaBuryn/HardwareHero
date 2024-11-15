using HardwareHero.Shared.Extensions;
using HardwareHero.Shared.Extensions.MongoDb;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureSecretsFile();

builder.Services.AddFluentValidation();
builder.Services.ConfigureOpenTelemetry(builder);

builder.Services.ConfigureCommonJwtAuthentication(builder);
builder.Services.ConfigurePolicyAuthorization();

builder.Services.ConfigureOptions<DatabaseOptions>(builder.Configuration, ConnectionNames.PricesConnection);
builder.Services.ConfigureDbContext(builder.Services.GetMongoDatabaseOptions());
builder.Services.ConfigureBusinessLogicLayer();

builder.Services.AddCustomControllers();

builder.Services.ConfigureSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureCORSPolicy();

IdentityModelEventSource.ShowPII = true;
builder.Host.ConfigureElasticLogging();
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

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization("ApiScope");

app.Run();
