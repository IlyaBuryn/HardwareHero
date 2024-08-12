using System.Net;
using HardwareHero.Filter.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HardwareHero.Shared.Middlewares
{
    public class ExceptionHandlerMiddleware<T>
    {
        private readonly ILogger<ExceptionHandlerMiddleware<T>> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(
            RequestDelegate next, 
            ILogger<ExceptionHandlerMiddleware<T>> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DataValidationException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex);
            }
            catch (AuthorizationException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Forbidden, ex);
            }
            catch (AlreadyExistException<T> ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Conflict, ex);
            }
            catch (PageOptionsValidationException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex);
            }
            catch (AuthenticationProblemException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex);
            }
            catch (AuthenticationException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Unauthorized, ex);
            }
            catch (FilterException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context, 
            HttpStatusCode statusCode, 
            Exception exception)
        {
            _logger.LogError(new EventId(), exception, exception.Message);

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = exception.Message
            };

            var json = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(json);
        }

        private class ErrorResponse
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }
    }
}
