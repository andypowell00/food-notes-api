namespace FoodDiary.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, ApiKeyValidator apiKeyValidator)
        {
            _next = next;
            _apiKey = apiKeyValidator.ApiKey;
        }

        public async Task Invoke(HttpContext context)
        {
            // Allow Swagger and health check endpoints without authentication
            var path = context.Request.Path;
            if (path.StartsWithSegments("/swagger") || path.StartsWithSegments("/health"))
            {
                await _next(context);
                return;
            }

            // Validate API key from request headers
            if (!context.Request.Headers.TryGetValue("Authorization", out var extractedApiKey) ||
                extractedApiKey != $"ApiKey {_apiKey}")
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Unauthorized: Invalid API Key.");
                return;
            }

            await _next(context);
        }
    }
}
