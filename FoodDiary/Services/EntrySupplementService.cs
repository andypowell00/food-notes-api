using FoodDiary.Data;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.Services
{
    public class EntrySupplementService : IEntrySupplementService
    {
        private readonly FoodDiaryContext _context;

        public EntrySupplementService(FoodDiaryContext context)
        {
            _context = context;
        }

        public async Task<List<EntrySupplement>> GetSupplementsByEntryIdAsync(int entryId)
        {
            return await _context.EntrySupplements
                .Include(ei => ei.Supplement) // Include related Supplement if needed
                .Where(ei => ei.EntryId == entryId)
                .ToListAsync();
        }

        public async Task<EntrySupplement?> GetEntrySupplementByIdAsync(int entryId, int SupplementId)
        {
            return await _context.EntrySupplements
                .Include(es => es.Supplement) // Include the Supplement navigation property
                .FirstOrDefaultAsync(ei => ei.EntryId == entryId && ei.SupplementId == SupplementId);
        }

        public async Task CreateEntrySupplementAsync(EntrySupplement entrySupplement)
        {
            _context.EntrySupplements.Add(entrySupplement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntrySupplementAsync(EntrySupplement entrySupplement)
        {
            _context.EntrySupplements.Remove(entrySupplement);
            await _context.SaveChangesAsync();
        }
    }
}
