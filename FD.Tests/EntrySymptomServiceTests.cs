using FoodDiary.Models;
using FoodDiary.Services;
using FoodDiary.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public class EntrySymptomServiceTests : IDisposable
{
    private readonly FoodDiaryContext _context;
    private readonly EntrySymptomService _service;

    public EntrySymptomServiceTests()
    {
        var options = new DbContextOptionsBuilder<FoodDiaryContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new FoodDiaryContext(options);
        _service = new EntrySymptomService(_context);
    }

    [Fact]
    public async Task GetSymptomsByEntryIdAsync_ShouldReturnSymptomsForEntry()
    {
        // Arrange
        var entry = new Entry { Id = 1 };
        var symptom = new Symptom { Title = "Headache" };
        _context.EntrySymptoms.Add(new EntrySymptom { EntryId = 1, Symptom = symptom });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetSymptomsByEntryIdAsync(1);

        // Assert
        Assert.Single(result);
        Assert.Equal("Headache", result[0].Symptom.Title);
    }

    [Fact]
    public async Task GetEntrySymptomByIdAsync_ShouldReturnCorrectEntrySymptom()
    {
        // Arrange
        var entrySymptom = new EntrySymptom
        {
            EntryId = 1,
            SymptomId = 1,
            Symptom = new Symptom { Id = 1, Title = "Fever" }
        };

        _context.EntrySymptoms.Add(entrySymptom);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetEntrySymptomByIdAsync(1, 1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Fever", result!.Symptom.Title);
    }

    [Fact]
    public async Task CreateEntrySymptomAsync_ShouldAddEntrySymptom()
    {
        // Arrange
        var entrySymptom = new EntrySymptom
        {
            EntryId = 1,
            Symptom = new Symptom { Title = "Cough" }
        };

        // Act
        await _service.CreateEntrySymptomAsync(entrySymptom);

        // Assert
        var result = await _context.EntrySymptoms.FirstOrDefaultAsync();
        Assert.NotNull(result);
        Assert.Equal("Cough", result!.Symptom.Title);
    }

    [Fact]
    public async Task UpdateEntrySymptomAsync_ShouldUpdateEntrySymptom()
    {
        // Arrange
        var entrySymptom = new EntrySymptom
        {
            EntryId = 1,
            SymptomId = 1,
            Symptom = new Symptom { Title = "Nausea" },
            Notes = "Initial Notes"
        };

        _context.EntrySymptoms.Add(entrySymptom);
        await _context.SaveChangesAsync();

        // Act
        entrySymptom.Notes = "Updated Notes";
        await _service.UpdateEntrySymptomAsync(entrySymptom);

        // Assert
        var updatedEntrySymptom = await _context.EntrySymptoms.FirstOrDefaultAsync();
        Assert.NotNull(updatedEntrySymptom);
        Assert.Equal("Updated Notes", updatedEntrySymptom!.Notes);
    }

    [Fact]
    public async Task DeleteEntrySymptomAsync_ShouldRemoveEntrySymptom()
    {
        // Arrange
        var entrySymptom = new EntrySymptom
        {
            EntryId = 1,
            SymptomId = 1,
            Symptom = new Symptom { Id = 1, Title = "Fatigue" }
        };

        _context.EntrySymptoms.Add(entrySymptom);
        await _context.SaveChangesAsync();

        // Act
        await _service.DeleteEntrySymptomAsync(entrySymptom);
        var deletedEntrySymptom = await _context.EntrySymptoms
            .FirstOrDefaultAsync(es => es.EntryId == 1 && es.SymptomId == 1);

        // Assert
        Assert.Null(deletedEntrySymptom);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
