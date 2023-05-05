using UserManagement.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDbContext(builder);

builder.Services.AddIdentity();

builder.Services.AddIdentityServerAuthentication();

builder.Services.AddIdentityServerAuthorization();

builder.Services.AddControllers();

var app = builder.Build();

app.MigrationInitialization();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers().RequireAuthorization("ApiScope");

app.Run();
