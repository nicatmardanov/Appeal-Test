using Core.Entities.Concrete;
using Core.Utilities.Constants;
using Core.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Core.Middlewares
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            int statusCode; string message;

            if (exception.GetType() == typeof(ValidationException))
            {
                message = exception.Message;
                statusCode = 400;
            }
            else
            {
                message = Messages.InternalServerError;
                statusCode = 500;
            }

            httpContext.Response.StatusCode = statusCode;
            return httpContext.Response.WriteAsync(new ErrorDetails() { StatusCode = statusCode, Message = message }.ToString());
        }
    }
}
