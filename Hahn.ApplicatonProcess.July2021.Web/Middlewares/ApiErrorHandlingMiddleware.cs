using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Hahn.ApplicationProcess.July2021.Domain.Exceptions;
using Hahn.ApplicationProcess.July2021.Web.Integration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Hahn.ApplicationProcess.July2021.Web.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ApiErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiErrorHandlingMiddleware(RequestDelegate next)
        { 
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.HasValue)
            {
                if (context.Request.Path.Value.StartsWith("/api/"))
                {
                    try
                    {
                        await _next(context);
                    }
                    catch (Exception e)
                    {
                        await HandleExceptionAsync(context, e);
                    }
                }
                else
                {
                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = 500;
            var error = "internal_error";
            var errorDescription = ex.Message; //"Internal Server Error";

            if (ex is DomainStateException)
            {
                code = 400;
                error = "bad_request";
                errorDescription = ex.Message;
            }
            else if (ex is ArgumentNullException)
            {
                code = 400;
                error = "bad_request";
                errorDescription = ex.Message;
            }
            else if (ex is NotFoundException)
            {
                code = 204;
                error = "bad_request";
                errorDescription = "object not found";
            }
            else if (ex is ValidationException)
            {
                var validationExp = (ValidationException) ex;
                code = 400;
                error = "bad_request";
                errorDescription = string.Join(',', validationExp.Errors.Select(x => x.ErrorMessage).Distinct());
            }

            

            var errorResponse = new ErrorResponse
            {
                error = error,
                error_description = errorDescription
            };
            var result = JsonConvert.SerializeObject(errorResponse);
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(result);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ApiErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiErrorHandlingMiddleware>();
        }
    }
}
