using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HardwareHero.Shared.Middlewares
{
    public class LoggingRequestsMiddleware
    {
        private readonly ILogger<LoggingRequestsMiddleware> _logger;
        private readonly RequestDelegate _next;

        public LoggingRequestsMiddleware(
            ILogger<LoggingRequestsMiddleware> logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Handling request: {context.Request.Path}");

            await _next(context);

            _logger.LogInformation("Finished handling request.");
        }
    }
}
