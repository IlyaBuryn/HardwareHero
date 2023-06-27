using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.Settings;
using Mail.BusinessLogic.Extensions;
using Mail.Api.Extensions;
using HardwareHero.Services.Shared.Middlewares;

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

var app = builder.Build();

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
app.MapControllers();

app.Run();
