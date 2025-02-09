using FoodDiary.Data;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.Services
{
    public class EntryIngredientService : IEntryIngredientService
    {
        private readonly FoodDiaryContext _context;

        public EntryIngredientService(FoodDiaryContext context)
        {
            _context = context;
        }

        public async Task<List<EntryIngredient>> GetIngredientsByEntryIdAsync(int entryId)
        {
            return await _context.EntryIngredients
                .Include(ei => ei.Ingredient) // Include related Ingredient if needed
                .Where(ei => ei.EntryId == entryId)
                .ToListAsync();
        }

        public async Task<EntryIngredient?> GetEntryIngredientByIdAsync(int entryId, int ingredientId)
        {
            return await _context.EntryIngredients
                .FirstOrDefaultAsync(ei => ei.EntryId == entryId && ei.IngredientId == ingredientId);
        }

        public async Task CreateEntryIngredientAsync(EntryIngredient entryIngredient)
        {
            _context.EntryIngredients.Add(entryIngredient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntryIngredientAsync(EntryIngredient entryIngredient)
        {
            _context.EntryIngredients.Update(entryIngredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntryIngredientAsync(EntryIngredient entryIngredient)
        {
            _context.EntryIngredients.Remove(entryIngredient);
            await _context.SaveChangesAsync();
        }
    }
}
