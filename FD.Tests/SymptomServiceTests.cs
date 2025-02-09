using FoodDiary.Models;
using FoodDiary.Services;
using FoodDiary.Data;
using Microsoft.EntityFrameworkCore;

    public class SymptomServiceTests : IDisposable
    {
        private readonly FoodDiaryContext _context;
        private readonly SymptomService _service;

        public SymptomServiceTests()
        {
            var options = new DbContextOptionsBuilder<FoodDiaryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new FoodDiaryContext(options);
            _service = new SymptomService(_context);
        }

        [Fact]
        public async Task GetAllSymptomsAsync_ShouldReturnAllSymptoms()
        {
            // Arrange
            _context.Symptoms.Add(new Symptom { Title = "Fever" });
            _context.Symptoms.Add(new Symptom { Title = "Cough" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAllSymptomsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateSymptomAsync_ShouldAddSymptom()
        {
            // Arrange
            var symptom = new Symptom { Title = "Fatigue" };

            // Act
            await _service.CreateSymptomAsync(symptom);

            // Assert
            var result = await _context.Symptoms.FirstOrDefaultAsync();
            Assert.NotNull(result);
            Assert.Equal("Fatigue", result.Title);
        }

        [Fact]
        public async Task GetSymptomByIdAsync_ShouldReturnCorrectSymptom()
        {
            // Arrange
            var symptom = new Symptom { Title = "Bloating" };
            _context.Symptoms.Add(symptom);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetSymptomByIdAsync(symptom.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Bloating", result.Title);
        }

        [Fact]
        public async Task DeleteSymptomAsync_ShouldRemoveSymptom()
        {
            // Arrange
            var symptom = new Symptom { Title = "Headache" };
            _context.Symptoms.Add(symptom);
            await _context.SaveChangesAsync();

            // Act
            await _service.DeleteSymptomAsync(symptom.Id);

            // Assert
            var deletedSymptom = await _context.Symptoms.FindAsync(symptom.Id);
            Assert.Null(deletedSymptom);
        }

        [Fact]
        public async Task UpdateSymptomAsync_ShouldUpdateSymptom()
        {
            // Arrange
            var Symptom = new Symptom { Title = "Fatigue" };
            _context.Symptoms.Add(Symptom);
            await _context.SaveChangesAsync();

            // Act
            Symptom.Title = "Fatigue";
            await _service.UpdateSymptomAsync(Symptom);

            // Assert
            var updatedSymptom = await _context.Symptoms.FindAsync(Symptom.Id);
            Assert.Equal("Fatigue", updatedSymptom?.Title);
        }

        [Fact]
        public async Task GetSymptomByIdAsync_ShouldReturnNullForNonExistentId()
        {
            // Act
            var result = await _service.GetSymptomByIdAsync(666);

            // Assert
            Assert.Null(result);
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
