using HardwareHero.Services.Shared.Constants;
using Contributor.BusinessLogic.Extensions;
using HardwareHero.Services.Shared.Settings;
using HardwareHero.Services.Shared.Middlewares;
using Contributor.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<ChatSettings>(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString(ConnectionNames.ContributorsConnection);
if (connectionString != null)
{
    builder.Services.ConfigureBusinessLogicLayer(connectionString);
}

builder.Services.AddIdentityServerAuthentication();

builder.Services.AddApiScopeAuthorization();

builder.Services.AddCors();

var app = builder.Build();

app.DatabaseInitialization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

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

app.Run();