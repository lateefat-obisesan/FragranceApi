using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace FragranceApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
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
            catch (KeyNotFoundException ex)
            {
                await WriteError(
                    context,
                    404,
                    "Not Found",
                    ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                await WriteError(
                    context,
                    409,
                    "Conflict",
                    ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");

                await WriteError(
                    context,
                    500,
                    "Internal Server Error",
                    "An unexpected error occurred.");
            }
        }

        private static async Task WriteError(
            HttpContext context,
            int statusCode,
            string title,
            string detail)
        {
            context.Response.StatusCode = statusCode;

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Type = "about:blank"
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
