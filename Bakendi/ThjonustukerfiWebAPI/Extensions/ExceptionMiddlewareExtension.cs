using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Extensions
{
    /// <summary>
    ///     Middleware that handles exceptions globally for the webhost application. Certain exceptions are logged to the database.
    ///     When exceptions are logged, the exception itself is logged as well as the stack trace.
    ///     
    ///     This method creates a new thread or threadpool to handle the exceptions.
    /// </summary>
    public class ExceptionMiddlewareExtension
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ExceptionMiddlewareExtension));
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

        /// <summary>
        ///     Handles all exceptions thrown, returns the correct status code given the exception, if the exception is unknown it will log the stacktrace to the database.
        /// </summary>
        private Task HandleExceptionAsync(HttpContext context, Exception exception, ILogService logService)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            _log.Info("Hello from the Exception MiddleWare");
            _log.Info("Some message", exception);

            if(exception != null)
            {
                if(exception is InvalidIdException)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.Conflict;

                    return context.Response.WriteAsync(exception.Message);
                }
                if(exception is NotFoundException)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.NotFound;

                    return context.Response.WriteAsync(exception.Message);
                }
                if(exception is BadRequestException)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;

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