using FoodDiary.Data;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.Services
{
    public class SupplementService : ISupplementService
    {
        private readonly FoodDiaryContext _context;

        public SupplementService(FoodDiaryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplement>> GetAllSupplementsAsync()
        {
            return await _context.Supplements.ToListAsync();
        }

        public async Task<Supplement?> GetSupplementByIdAsync(int id)
        {
            return await _context.Supplements.FindAsync(id);
        }

        public async Task CreateSupplementAsync(Supplement Supplement)
        {
            _context.Supplements.Add(Supplement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSupplementAsync(int id)
        {
            var Supplement = await _context.Supplements.FindAsync(id);
            if (Supplement != null)
            {
                _context.Supplements.Remove(Supplement);
                await _context.SaveChangesAsync();
            }
        }
    }
}
