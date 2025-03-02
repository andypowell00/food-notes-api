using System.Net;
using System.Text.Json;


namespace FoodDiary.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Process the request
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred."); // Log the error
                await HandleExceptionAsync(context, ex); // Handle the error
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
            return Task.CompletedTask;
        }
    }
}
