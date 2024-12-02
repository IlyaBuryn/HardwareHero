using Gateway.Ocelot.Middlewares;
using Ocelot.DependencyInjection;

namespace Gateway.Ocelot.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplicationBuilder ConfigureOcelotFiles(
            this WebApplicationBuilder builder)
        {
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddOcelot("./Schemas", null)
                .AddEnvironmentVariables();

            return builder;
        }

        public static IApplicationBuilder UseRefreshTokenMiddleware(
            this IApplicationBuilder app)
        {
            app
                .UseMiddleware<RefreshTokenMiddleware>();

            return app;
        }
    }
}
