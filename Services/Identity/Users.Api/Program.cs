using Users.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureSecretsFile();

builder.Services.ConfigureFluentValidation();
builder.Services.ConfigureOpenTelemetry(builder);

builder.Services.ConfigureSQLServerContexts(builder);
builder.Services.ConfigureCommonJwtAuthentication(builder);
builder.Services.ConfigurePolicyAuthorization();
builder.Services.ConfigureCustomIdentity();

builder.Services.ConfigureServices();

builder.Services.AddCustomControllers();

builder.Services.ConfigureSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureCORSPolicy();

builder.Host.ConfigureElasticLogging();
var app = builder.Build();

app.UseMigration<UsersDbContext>("Identity.Shared");
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
