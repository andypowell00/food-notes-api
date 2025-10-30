using FoodDiary.Data;
using FoodDiary.Middleware;
using FoodDiary.Services;
using FoodDiary.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.Helpers
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all required services to the service collection
        /// </summary>
        public static IServiceCollection AddFoodDiaryServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Get configuration values
            var allowedOrigins = ConfigHelper.GetCorsOrigins(configuration);
            var apiKey = ConfigHelper.GetRequiredConfig(configuration, "ApiKey", "FOOD_DIARY_API_KEY");
            var dbConnection = ConfigHelper.GetRequiredConfig(configuration, "ConnectionStrings:DefaultConnection", "DATABASE_CONNECTION");

            // Configure Database
            services.AddDbContext<FoodDiaryContext>(options =>
                options.UseSqlite(dbConnection));

            // Register API Key Validator
            services.AddSingleton(new ApiKeyValidator(apiKey));

            // Register database backup helper
            services.AddSingleton<DbBackupHelper>();

            // Register domain services
            AddDomainServices(services);
            
            // Configure API behavior
            ConfigureApiBehavior(services);
            
            // Add Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            
            // Add AutoMapper
            services.AddAutoMapper(typeof(Program));
            
            // Configure CORS
            ConfigureCors(services, allowedOrigins);
            
            return services;
        }
        
        private static void AddDomainServices(IServiceCollection services)
        {
            services.AddScoped<IEntryService, EntryService>();
            services.AddScoped<ISymptomService, SymptomService>();
            services.AddScoped<ISupplementService, SupplementService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IEntryIngredientService, EntryIngredientService>();
            services.AddScoped<IEntrySymptomService, EntrySymptomService>();
            services.AddScoped<IEntrySupplementService, EntrySupplementService>();
            services.AddScoped<ISafeIngredientService, SafeIngredientService>();
            services.AddScoped<IUnsafeIngredientService, UnsafeIngredientService>();
            services.AddScoped<IMealService, MealService>();
            services.AddScoped<IEntryMealService, EntryMealService>();
        }
        
        private static void ConfigureApiBehavior(IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        // Log validation errors 
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                        logger.LogWarning("Validation failed: {@ValidationErrors}", context.ModelState);

                        // Return empty 500 response
                        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        return new EmptyResult();
                    };
                });
        }
        
        private static void ConfigureCors(IServiceCollection services, string[] allowedOrigins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DevelopmentCors", policy =>
                {
                    policy.AllowAnyOrigin() // Allow all in development
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });

                options.AddPolicy("ProductionCors", policy =>
                {
                    policy.WithOrigins(allowedOrigins) // Specify allowed domains
                          .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                          .WithHeaders("Content-Type", "Authorization"); // Restrict headers
                });
            });
        }
    }
}
