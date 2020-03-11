using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Extensions
{
    public class ExceptionMiddlewareExtension
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddlewareExtension(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, ILogService logservice)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logservice);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, ILogService logService)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            if(exception != null)
            {
                if(exception is InvalidIdException)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.Conflict;

                    return context.Response.WriteAsync(exception.Message);
                }
            }

            logService.LogToDatabase(new ExceptionModel
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                StackTrace = exception.StackTrace
            });
            return context.Response.WriteAsync("Generic error");
        }
    }
}