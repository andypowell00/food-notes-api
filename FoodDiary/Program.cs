using FoodDiary.Middleware;
using FoodDiary.Helpers;


var builder = WebApplication.CreateBuilder(args);

// Add services using the helper extension method
builder.Services.AddFoodDiaryServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

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
    // Use custom API key authentication middleware only in non-development environments
    app.UseMiddleware<ApiKeyMiddleware>();
}

// Enable Routing and Map Controllers
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
