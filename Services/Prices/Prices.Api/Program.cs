using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.Middlewares;
using HardwareHero.Services.Shared.Settings;
using Prices.BusinessLogic.Extensions;
using Prices.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomControllers();

builder.Services.AddFluentValidation();

builder.Services.ConfigureOptions<DatabaseSettings>(
    builder.Configuration,
    ConnectionNames.PricesConnection);

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
