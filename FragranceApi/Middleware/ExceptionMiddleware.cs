using System.Text.Json;

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
                context.Response.StatusCode = 404;
                await WriteError(context, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                context.Response.StatusCode = 409;
                await WriteError(context, ex.Message);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");

                context.Response.StatusCode = 500;
                await WriteError(
                    context,
                    "An unexpected error occurred.");
            }
        }

        private static async Task WriteError(
          HttpContext context,
          string message)
        {
            context.Response.ContentType = "application/problem+json";

            var problem = new
            {
                type = "about:blank",
                title = "An error occurred",
                status = context.Response.StatusCode,
                detail = message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(problem));
        }
    }
}


