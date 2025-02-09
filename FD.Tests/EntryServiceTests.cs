using FoodDiary.Models;
using FoodDiary.Services;
using FoodDiary.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;

public class EntryServiceTests : IDisposable
{
    private readonly FoodDiaryContext _context;
    private readonly EntryService _service;

    public EntryServiceTests()
    {
        var options = new DbContextOptionsBuilder<FoodDiaryContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new FoodDiaryContext(options);
        _service = new EntryService(_context);
    }

    [Fact]
    public async Task GetAllEntriesAsync_ShouldReturnAllEntries()
    {
        // Arrange
        _context.Entries.Add(new Entry { Date = DateTime.Now });
        _context.Entries.Add(new Entry { Date = DateTime.Now.AddDays(-1) });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAllEntriesAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task CreateEntryAsync_ShouldAddEntry()
    {
        // Arrange
        var entry = new Entry { Date = DateTime.Now };

        // Act
        await _service.CreateEntryAsync(entry);

        // Assert
        var result = await _context.Entries.FirstOrDefaultAsync();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetEntryByIdAsync_ShouldReturnCorrectEntry()
    {
        // Arrange
        var entry = new Entry { Date = DateTime.Now };
        _context.Entries.Add(entry);
        await _context.SaveChangesAsync();

        // Act
        var retrievedEntry = await _service.GetEntryByIdAsync(entry.Id);

        // Assert
        Assert.NotNull(retrievedEntry);
        Assert.Equal(entry.Id, retrievedEntry!.Id);
    }

    [Fact]
    public async Task UpdateEntryAsync_ShouldUpdateEntry()
    {
        // Arrange
        var entry = new Entry { Date = DateTime.Now };
        _context.Entries.Add(entry);
        await _context.SaveChangesAsync();

        // Act
        entry.Date = DateTime.Now.AddDays(1); // Modify the entry
        await _service.UpdateEntryAsync(entry);
        var updatedEntry = await _context.Entries.FirstOrDefaultAsync(e => e.Id == entry.Id);

        // Assert
        Assert.NotNull(updatedEntry);
        Assert.Equal(entry.Date, updatedEntry!.Date);
    }

    [Fact]
    public async Task DeleteEntryAsync_ShouldRemoveEntry()
    {
        // Arrange
        var entry = new Entry { Date = DateTime.Now };
        _context.Entries.Add(entry);
        await _context.SaveChangesAsync();

        // Act
        await _service.DeleteEntryAsync(entry.Id);
        var deletedEntry = await _context.Entries.FirstOrDefaultAsync(e => e.Id == entry.Id);

        // Assert
        Assert.Null(deletedEntry);
    }

    [Fact]
    public async Task GetSymptomaticEntriesAsync_ShouldReturnSymptomaticEntries()
    {
        // Arrange
        _context.Entries.Add(new Entry { Date = DateTime.Now, Symptomatic = true });
        _context.Entries.Add(new Entry { Date = DateTime.Now.AddDays(-1), Symptomatic = false });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetSymptomaticEntriesAsync();

        // Assert
        Assert.Single(result);
        Assert.True(result.First().Symptomatic);
    }

    [Fact]
    public async Task GetEntryByIdAsync_ShouldReturnNullForNonExistentEntry()
    {
        // Act
        var result = await _service.GetEntryByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
