namespace FoodDiary.Helpers
{
    public static class ConfigHelper
    {
        public static string GetRequiredConfig(IConfiguration configuration, string key, string envVarName)
        {
            // Prioritize environment variable over appsettings.json
            var value = Environment.GetEnvironmentVariable(envVarName) ?? configuration[key];

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException($"Configuration setting '{key}' or environment variable '{envVarName}' is missing!");
            }

            return value;
        }

        public static string[] GetCorsOrigins(IConfiguration configuration)
        {
            // Prioritize environment variable for CORS origins
            var origins = Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGINS") ??
                          configuration["CorsSettings:AllowedOrigins"] ??
                          "http://localhost:3000"; // Default fallback for local dev

            return origins.Split(';', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
