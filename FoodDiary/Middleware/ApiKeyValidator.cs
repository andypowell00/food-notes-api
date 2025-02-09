namespace FoodDiary.Middleware
{
    public class ApiKeyValidator
    {
        public string ApiKey { get; }

        public ApiKeyValidator(string apiKey)
        {
            ApiKey = apiKey;
        }
    }
}
