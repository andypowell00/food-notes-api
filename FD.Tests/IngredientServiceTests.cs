using FoodDiary.Data;
using FoodDiary.Models;
using FoodDiary.Services;
using Microsoft.EntityFrameworkCore;

public class IngredientServiceTests : IDisposable
{
    private readonly FoodDiaryContext _context;
    private readonly IngredientService _service;

    public IngredientServiceTests()
    {
        // Create a new in-memory database for each test
        var options = new DbContextOptionsBuilder<FoodDiaryContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique database name per test
            .Options;

        _context = new FoodDiaryContext(options);
        _service = new IngredientService(_context);
    }

    [Fact]
    public async Task GetAllIngredientsAsync_ShouldReturnEmptyList_WhenNoIngredients()
    {
        // Act
        var result = await _service.GetAllIngredientsAsync();

        // Assert
        Assert.Empty(result); // Assert that the result is empty
    }

    [Fact]
    public async Task CreateIngredientAsync_ShouldAddIngredientToDatabase()
    {
        // Arrange
        var ingredient = new Ingredient { Name = "Tomato" };

        // Act
        await _service.CreateIngredientAsync(ingredient);
        var result = await _context.Ingredients.FirstOrDefaultAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Tomato", result.Name);
    }

    [Fact]
    public async Task GetAllIngredientsAsync_ShouldReturnAllIngredients()
    {
        // Arrange
        _context.Ingredients.Add(new Ingredient { Name = "Salt" });
        _context.Ingredients.Add(new Ingredient { Name = "Sugar" });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAllIngredientsAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, i => i.Name == "Salt");
        Assert.Contains(result, i => i.Name == "Sugar");
    }

    [Fact]
    public async Task GetIngredientByIdAsync_ShouldReturnCorrectIngredient()
    {
        // Arrange
        var ingredient = new Ingredient { Name = "Pepper" };
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetIngredientByIdAsync(ingredient.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Pepper", result.Name);
    }

    [Fact]
    public async Task GetIngredientByIdAsync_ShouldReturnNullForNonExistentId()
    {
        // Act
        var result = await _service.GetIngredientByIdAsync(999);

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public async Task DeleteIngredientAsync_ShouldRemoveIngredient()
    {
        // Arrange
        var ingredient = new Ingredient { Name = "Honey" };
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();

        // Act
        await _service.DeleteIngredientAsync(ingredient.Id);

        // Assert
        var deletedIngredient = await _context.Ingredients.FindAsync(ingredient.Id);
        Assert.Null(deletedIngredient);
    }

    [Fact]
    public async Task UpdateIngredientAsync_ShouldUpdateIngredient()
    {
        // Arrange
        var ingredient = new Ingredient { Name = "Flour" };
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();

        // Act
        ingredient.Name = "Whole Wheat Flour";
        await _service.UpdateIngredientAsync(ingredient);

        // Assert
        var updatedIngredient = await _context.Ingredients.FindAsync(ingredient.Id);
        Assert.Equal("Whole Wheat Flour", updatedIngredient?.Name);
    }


    public void Dispose()
    {
        // Dispose of the context to release in-memory database resources
        _context.Dispose();
    }
}
