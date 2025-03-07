using FoodDiary.Models;
using FoodDiary.Services;
using FoodDiary.Data;
using Microsoft.EntityFrameworkCore;


public class EntryIngredientServiceTests : IDisposable
{
    private readonly FoodDiaryContext _context;
    private readonly EntryIngredientService _service;

    public EntryIngredientServiceTests()
    {
        var options = new DbContextOptionsBuilder<FoodDiaryContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new FoodDiaryContext(options);
        _service = new EntryIngredientService(_context);
    }

    [Fact]
    public async Task GetIngredientsByEntryIdAsync_ShouldReturnIngredientsForGivenEntry()
    {
        // Arrange
        var entry = new Entry { Id = 1 };
        var ingredient = new Ingredient { Id = 1, Name = "Salt" };
        _context.EntryIngredients.Add(new EntryIngredient { EntryId = 1, Ingredient = ingredient });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetIngredientsByEntryIdAsync(1);

        // Assert
        Assert.Single(result);
        Assert.Equal("Salt", result[0].Ingredient.Name);
    }

    [Fact]
    public async Task GetEntryIngredientByIdAsync_ShouldReturnCorrectEntryIngredient()
    {
        // Arrange
        var entryIngredient = new EntryIngredient
        {
            EntryId = 1,
            IngredientId = 1,
            Ingredient = new Ingredient { Id = 1, Name = "Sugar" }
        };

        _context.EntryIngredients.Add(entryIngredient);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetEntryIngredientByIdAsync(1, 1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Sugar", result!.Ingredient.Name);
    }

    [Fact]
    public async Task CreateEntryIngredientAsync_ShouldAddEntryIngredient()
    {
        // Arrange
        var entryIngredient = new EntryIngredient
        {
            EntryId = 1,
            Ingredient = new Ingredient { Name = "Sugar" }
        };

        // Act
        await _service.CreateEntryIngredientAsync(entryIngredient);

        // Assert
        var result = await _context.EntryIngredients.FirstOrDefaultAsync();
        Assert.NotNull(result);
        Assert.Equal("Sugar", result!.Ingredient.Name);
    }

    [Fact]
    public async Task UpdateEntryIngredientAsync_ShouldUpdateEntryIngredient()
    {
        // Arrange
        var entryIngredient = new EntryIngredient
        {
            EntryId = 1,
            IngredientId = 1,
            Ingredient = new Ingredient { Name = "Salt" },
            Notes = "Initial Notes"
        };

        _context.EntryIngredients.Add(entryIngredient);
        await _context.SaveChangesAsync();

        // Act
        entryIngredient.Notes = "Updated Notes";
        await _service.UpdateEntryIngredientAsync(entryIngredient);

        // Assert
        var updatedEntryIngredient = await _context.EntryIngredients.FirstOrDefaultAsync();
        Assert.NotNull(updatedEntryIngredient);
        Assert.Equal("Updated Notes", updatedEntryIngredient!.Notes);
    }

    [Fact]
    public async Task DeleteEntryIngredientAsync_ShouldRemoveEntryIngredient()
    {
        // Arrange
        var entryIngredient = new EntryIngredient
        {
            EntryId = 1,
            IngredientId = 1,
            Ingredient = new Ingredient { Id = 1, Name = "Sugar" }
        };

        _context.EntryIngredients.Add(entryIngredient);
        await _context.SaveChangesAsync();

        // Act
        await _service.DeleteEntryIngredientAsync(entryIngredient);
        var deletedEntryIngredient = await _context.EntryIngredients
            .FirstOrDefaultAsync(ei => ei.EntryId == 1 && ei.IngredientId == 1);

        // Assert
        Assert.Null(deletedEntryIngredient);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
