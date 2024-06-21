using HardwareHero.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomControllers();

builder.Services.AddFluentValidation();

builder.Services.ConfigureOptions<DatabaseOptions>(
    builder.Configuration,
    ConnectionNames.MailConnection);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.ConfigureBusinessLogicLayer();

builder.Services.AddIdentityServerAuthentication();

builder.Services.AddApiScopeAuthorization();

builder.Services.AddCors();

builder.Services.ConfigureCommonOpenTelemetry(
    "MainRemoteManage",
    builder.Configuration.GetValue<string>("OpenRemoteManageMeterName"),
    builder.Configuration["Otel:Endpoint"]);

builder.Host.ConfigureElasticLogging();

var app = builder.Build();

app.UseCommonCustomMiddlewares();

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
app.MapControllers();

app.Run();
