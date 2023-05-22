using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.Middlewares;
using IdentityServer.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        builder.Services.AddControllers();

        builder.Services.SetupUserContext(
            builder.Configuration.GetConnectionString(ConnectionNames.UsersConnection));

        builder.Services.AddCustomIdentity();

        builder.Services.AddCustomIdentityServer(
            builder.Configuration.GetConnectionString(ConnectionNames.IdentityServerConnection));
        
        builder.Services.AddCustomAuthentication();

        builder.Services.AddDefaultCorsPolicy();

        builder.Services.AddMvcCore();

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.MigrationInitialization();
        app.UseMiddleware<ExceptionHandlerMiddleware>();

        app.UseCors("default");

        if (app.Environment.IsDevelopment())
        {
            //app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapControllers();
        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}