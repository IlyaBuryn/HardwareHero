using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;

builder.Services.AddCustomControllers();

builder.Services.AddFluentValidation();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<PageSizeOptions>(builder.Configuration);
builder.Services.ConfigureOptions<ImagesSaveOptions>(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString(ConnectionNames.ContributorsConnection);
if (connectionString != null)
{
    builder.Services.ConfigureBusinessLogicLayer(connectionString);
}

builder.Services.AddIdentityServerAuthentication();

builder.Services.AddApiScopeAuthorization();

builder.Services.AddCors();

var app = builder.Build();

await app.DatabaseInitialization();
app.UseMiddleware<ExceptionHandlerMiddleware<BaseEntity>>();
app.UseHttpsRedirection();
app.UseRouting();

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
app.MapControllers().RequireAuthorization("ApiScope");

app.Run();