using HardwareHero.Services.Shared.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HardwareHero.Services.Shared.Middlewares
{
    public class ExceptionHandlerMiddleware<T>
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DataValidationException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (AuthorizationException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Forbidden, ex.Message);
            }
            catch (AlreadyExistException<T> ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Conflict, ex.Message);
            }
            catch (PageOptionsValidationException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (AuthenticationException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = message
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
