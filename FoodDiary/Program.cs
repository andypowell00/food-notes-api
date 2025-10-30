using FoodDiary.Data;
using FoodDiary.Helpers;
using FoodDiary.Middleware;
using Microsoft.EntityFrameworkCore;


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
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FoodDiaryContext>();
    db.Database.Migrate();
}

// Enable Routing and Map Controllers
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
