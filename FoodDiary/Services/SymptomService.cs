using FoodDiary.Data;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.Services
{
    public class SymptomService : ISymptomService
    {
        private readonly FoodDiaryContext _context;

        public SymptomService(FoodDiaryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Symptom>> GetAllSymptomsAsync()
        {
            return await _context.Symptoms.ToListAsync<Symptom>();
        }

        public async Task<Symptom?> GetSymptomByIdAsync(int id)
        {
            return await _context.Symptoms.FindAsync(id);
        }

        public async Task CreateSymptomAsync(Symptom symptom)
        {
            _context.Symptoms.Add(symptom);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSymptomAsync(Symptom symptom)
        {
            _context.Symptoms.Update(symptom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSymptomAsync(int id)
        {
            var symptom = await _context.Symptoms.FindAsync(id);
            if (symptom != null)
            {
                _context.Symptoms.Remove(symptom);
                await _context.SaveChangesAsync();
            }
        }
    }
}
