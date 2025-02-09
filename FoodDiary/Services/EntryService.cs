using FoodDiary.Data;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.Services
{
    public class EntryService : IEntryService
    {
        private readonly FoodDiaryContext _context;

        public EntryService(FoodDiaryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entry>> GetAllEntriesAsync()
        {
            return await _context.Entries
                .Include(e => e.EntrySymptoms)
                    .ThenInclude(es => es.Symptom)
                .Include(e => e.EntryIngredients)
                    .ThenInclude(ei => ei.Ingredient)
                .ToListAsync();
        }

        public async Task<Entry?> GetEntryByIdAsync(int id)
        {
            return await _context.Entries
                .Include(e => e.EntrySymptoms).ThenInclude(es => es.Symptom)
                .Include(e => e.EntryIngredients).ThenInclude(ei => ei.Ingredient)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateEntryAsync(Entry entry)
        {
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntryAsync(Entry entry)
        {
            _context.Entries.Update(entry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntryAsync(int id)
        {
            var entry = await _context.Entries.FindAsync(id);
            if (entry != null)
            {
                _context.Entries.Remove(entry);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Entry>> GetSymptomaticEntriesAsync()
        {
            return await _context.Entries.Where(e => e.Symptomatic).ToListAsync<Entry>();
        }
    }
}
