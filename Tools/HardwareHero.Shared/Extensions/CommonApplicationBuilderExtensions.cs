using HardwareHero.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HardwareHero.Shared.Extensions
{
    public static class CommonApplicationBuilderExtensions
    {
        public static void UseMigration<TContext>(this IApplicationBuilder app) where TContext : DbContext
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<TContext>()!
                    .Database.Migrate();
            }
        }

        public static void UseCommonCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware<BaseEntity>>();
            app.UseMiddleware<LoggingRequestsMiddleware>();
        }
    }
}
