using FoodDiary.Data;
using FoodDiary.Services;
using FoodDiary.Services.Abstract;
using FoodDiary.Middleware;
using Microsoft.EntityFrameworkCore;
using FoodDiary.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Read API key from configuration, env in prod
var allowedOrigins = ConfigHelper.GetCorsOrigins(builder.Configuration);
var apiKey = ConfigHelper.GetRequiredConfig(builder.Configuration, "ApiKey", "FOOD_DIARY_API_KEY");
var dbConnection = ConfigHelper.GetRequiredConfig(builder.Configuration, "ConnectionStrings:DefaultConnection", "DATABASE_CONNECTION");

// Configure Database
builder.Services.AddDbContext<FoodDiaryContext>(options =>
    options.UseSqlite(dbConnection));


builder.Services.AddSingleton(new ApiKeyValidator(apiKey));

// Register services
builder.Services.AddScoped<IEntryService, EntryService>();
builder.Services.AddScoped<ISymptomService, SymptomService>();
builder.Services.AddScoped<ISupplementService, SupplementService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IEntryIngredientService, EntryIngredientService>();
builder.Services.AddScoped<IEntrySymptomService, EntrySymptomService>();
builder.Services.AddScoped<IEntrySupplementService, EntrySupplementService>();
builder.Services.AddScoped<ISafeIngredientService, SafeIngredientService>();
builder.Services.AddScoped<IUnsafeIngredientService, UnsafeIngredientService>();

// Add Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

// Add CORS policies
builder.Services.AddCors(options =>
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

var app = builder.Build();

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevelopmentCors");
}
else
{
    app.UseCors("ProductionCors");
}

// Use custom API key authentication middleware
app.UseMiddleware<ApiKeyMiddleware>();


// Enable Routing and Map Controllers
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
