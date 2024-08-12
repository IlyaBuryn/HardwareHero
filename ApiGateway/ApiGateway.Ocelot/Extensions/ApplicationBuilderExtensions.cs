using ApiGateway.Ocelot.Middlewares;
using Ocelot.DependencyInjection;

namespace ApiGateway.Ocelot.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureOcelotFile(this WebApplicationBuilder builder)
        {
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddOcelot("Schemas", builder.Environment)
                .AddEnvironmentVariables();
        }

        public static void UseTokenRefreshMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<TokenRefreshMiddleware>();
        }
    }
}
