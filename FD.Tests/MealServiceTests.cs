using FoodDiary.Data;
using FoodDiary.Models;
using FoodDiary.Services;
using Microsoft.EntityFrameworkCore;

namespace FD.Tests
{
    public class MealServiceTests : IDisposable
    {
        private readonly FoodDiaryContext _context;
        private readonly MealService _service;

        public MealServiceTests()
        {
            // Create a new in-memory database for each test
            var options = new DbContextOptionsBuilder<FoodDiaryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique database name per test
                .Options;

            _context = new FoodDiaryContext(options);
            _service = new MealService(_context);
        }

        [Fact]
        public async Task GetAllMealsAsync_ShouldReturnEmptyList_WhenNoMeals()
        {
            // Act
            var result = await _service.GetAllMealsAsync();
            // Assert
            Assert.Empty(result); // Assert that the result is empty
        }
        [Fact]
        public async Task CreateMealAsync_ShouldAddMealToDatabase()
        {
            // Arrange
            var meal = new Meal { Name = "Breakfast" };
            // Act
            await _service.CreateMealAsync(meal, new List<int>());
            var result = await _context.Meals.FirstOrDefaultAsync();
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Breakfast", result.Name);
        }

        [Fact]
        public async Task GetAllMealsAsync_ShouldReturnAllMeals()
        {
            // Arrange
            _context.Meals.Add(new Meal { Name = "Lunch" });
            _context.Meals.Add(new Meal { Name = "Dinner" });
            await _context.SaveChangesAsync();
            // Act
            var result = await _service.GetAllMealsAsync();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, m => m.Name == "Lunch");
            Assert.Contains(result, m => m.Name == "Dinner");
        }
        [Fact]
        public async Task GetMealByIdAsync_ShouldReturnMeal_WhenMealExists()
        {
            // Arrange
            var meal = new Meal { Name = "Snack" };
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
            // Act
            var result = await _service.GetMealByIdAsync(meal.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Snack", result.Name);
        }
        [Fact]
        public async Task GetMealByIdAsync_ShouldReturnNull_WhenMealDoesNotExist()
        {
            // Act
            var result = await _service.GetMealByIdAsync(999); // Non-existent ID
            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task DeleteMealAsync_ShouldRemoveMealFromDatabase()
        {
            // Arrange
            var meal = new Meal { Name = "Brunch" };
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
            // Act
            await _service.DeleteMealAsync(meal.Id);
            var result = await _context.Meals.FindAsync(meal.Id);
            // Assert
            Assert.Null(result); // Meal should be deleted
        }
        [Fact]
        public async Task AddIngredientAsync_ShouldAddIngredientToMeal()
        {
            // Arrange
            var meal = new Meal { Name = "Salad" };
            var ingredient = new Ingredient { Name = "Lettuce" };
            _context.Meals.Add(meal);
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            // Act
            await _service.AddIngredient(meal.Id, ingredient.Id);
            var result = await _context.MealIngredients
                .FirstOrDefaultAsync(mi => mi.MealId == meal.Id && mi.IngredientId == ingredient.Id);
            // Assert
            Assert.NotNull(result); // Ingredient should be added to the meal
        }


        public void Dispose()
        {
            // Dispose of the context to release in-memory database resources
            _context.Dispose();
        }
    }
}
