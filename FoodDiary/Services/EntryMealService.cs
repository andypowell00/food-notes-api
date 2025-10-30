using FoodDiary.Data;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.Services
{
    public class EntryMealService : IEntryMealService
    {
        private readonly FoodDiaryContext _context;

        public EntryMealService(FoodDiaryContext context)
        {
            _context = context;
        }

        public async Task<List<EntryMeal>> GetMealsByEntryIdAsync(int entryId)
        {
            return await _context.EntryMeals
                .Include(em => em.Meal) // Include Meal for UI info
                .ThenInclude(m => m.MealIngredients)
                .ThenInclude(mi => mi.Ingredient)
                .Where(em => em.EntryId == entryId)
                .ToListAsync();
        }

        public async Task<EntryMeal?> GetEntryMealByIdAsync(int entryId, int mealId)
        {
            return await _context.EntryMeals
                .Include(em => em.Meal)
                .ThenInclude(m => m.MealIngredients)
                .ThenInclude(mi => mi.Ingredient)
                .FirstOrDefaultAsync(em => em.EntryId == entryId && em.MealId == mealId);
        }

        public async Task CreateEntryMealAsync(EntryMeal entryMeal)
        {
            _context.EntryMeals.Add(entryMeal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntryMealAsync(EntryMeal entryMeal)
        {
            _context.EntryMeals.Remove(entryMeal);
            await _context.SaveChangesAsync();
        }
    }
}
