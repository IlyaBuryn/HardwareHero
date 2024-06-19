using HardwareHero.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace HardwareHero.Shared.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseMyCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware<BaseEntity>>();
            app.UseMiddleware<LoggingRequestsMiddleware>();
        }
    }
}
