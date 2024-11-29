using Users.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureSecretsFile()
    .ConfigureElasticLogging();

builder.Services
    .ConfigureServices()
    .ConfigureEventServices(builder)
    .ConfigureEventHandlers();

builder.Services
    .ConfigureFluentValidation()
    .ConfigureOpenTelemetry(builder)
    .ConfigureSQLServerContexts(builder)
    .ConfigureCommonJwtAuthentication(builder)
    .ConfigurePolicyAuthorization()
    .ConfigureCustomIdentity()
    .ConfigureSwagger()
    .ConfigureCORSPolicy()
    .ConfigureCustomControllers();

builder.Services
    .AddEndpointsApiExplorer();

var app = builder.Build();

app.UseMigration<UsersDbContext>("Identity.Shared");
await app.SetupDefaultDataAsync();

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
