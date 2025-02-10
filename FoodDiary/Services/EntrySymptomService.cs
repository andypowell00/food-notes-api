using FoodDiary.Data;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.Services
{
    public class EntrySymptomService : IEntrySymptomService
    {
        private readonly FoodDiaryContext _context;

        public EntrySymptomService(FoodDiaryContext context)
        {
            _context = context;
        }

        public async Task<List<EntrySymptom>> GetSymptomsByEntryIdAsync(int entryId)
        {
            return await _context.EntrySymptoms
                .Include(es => es.Symptom) // Include related Symptom if needed
                .Where(es => es.EntryId == entryId)
                .ToListAsync();
        }

        public async Task<EntrySymptom?> GetEntrySymptomByIdAsync(int entryId, int symptomId)
        {
            return await _context.EntrySymptoms
                .FirstOrDefaultAsync(es => es.EntryId == entryId && es.SymptomId == symptomId);
        }

        public async Task CreateEntrySymptomAsync(EntrySymptom entrySymptom)
        {
            _context.EntrySymptoms.Add(entrySymptom);

            // Update Symptomatic flag
            var entry = await _context.Entries.FindAsync(entrySymptom.EntryId);
            if (entry != null)
            {
                entry.Symptomatic = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntrySymptomAsync(EntrySymptom entrySymptom)
        {
            _context.EntrySymptoms.Update(entrySymptom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntrySymptomAsync(EntrySymptom entrySymptom)
        {
            _context.EntrySymptoms.Remove(entrySymptom);

            // Check if any symptoms remain
            var remainingSymptoms = await _context.EntrySymptoms
                .CountAsync(es => es.EntryId == entrySymptom.EntryId);

            if (remainingSymptoms == 0)
            {
                var entry = await _context.Entries.FindAsync(entrySymptom.EntryId);
                if (entry != null)
                {
                    entry.Symptomatic = false;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
