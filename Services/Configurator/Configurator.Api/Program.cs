using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.Settings;
using Configurator.BusinessLogic.Extensions;
using Configurator.Api.Extensions;
using HardwareHero.Services.Shared.Middlewares;
using MongoDB.Bson.Serialization.Conventions;

var builder = WebApplication.CreateBuilder(args);

var pack = new ConventionPack
{
    new IgnoreIfNullConvention(true)
};
ConventionRegistry.Register("IgnoreIfNull", pack, t => true);

builder.Services.AddCustomControllers();

builder.Services.ConfigureOptions<DatabaseSettings>(
    builder.Configuration, 
    ConnectionNames.ConfiguratorConnection);

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
