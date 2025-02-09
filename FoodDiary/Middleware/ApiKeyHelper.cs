namespace FoodDiary.Middleware
{
    public static class ApiKeyHelper
    {
        public static string GetApiKey(IConfiguration configuration)
        {
            // Read API key from configuration or environment variable
            var apiKey = configuration["ApiKey"] ?? Environment.GetEnvironmentVariable("FOOD_DIARY_API_KEY");

            // Ensure API key is not null or empty
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException("API key is missing. Set it in appsettings.json or as an environment variable.");
            }

            return apiKey;
        }
    }
}
