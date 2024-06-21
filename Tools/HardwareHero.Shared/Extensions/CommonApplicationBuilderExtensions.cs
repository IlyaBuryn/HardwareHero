using HardwareHero.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace HardwareHero.Shared.Extensions
{
    public static class CommonApplicationBuilderExtensions
    {
        public static void UseCommonCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware<BaseEntity>>();
            app.UseMiddleware<LoggingRequestsMiddleware>();
        }
    }
}
