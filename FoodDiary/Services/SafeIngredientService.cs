using FoodDiary.Data;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.Services
{
    public class SafeIngredientService : ISafeIngredientService
    {
        private readonly FoodDiaryContext _context;

        public SafeIngredientService(FoodDiaryContext context)
        {
            _context = context;
        }
        public async Task<List<SafeIngredient>> GetSafeIngredientsAsync()
        {
            return await _context.SafeIngredients
                                 .Include(si => si.Ingredient) 
                                 .ToListAsync();
        }

        public async Task AddSafeIngredientAsync(Ingredient ingredient)
        {
            var safeIngredient = new SafeIngredient
            {
                IngredientId = ingredient.Id // Associate the SafeIngredient with the given Ingredient
            };

            _context.SafeIngredients.Add(safeIngredient);
            await _context.SaveChangesAsync();
        }
    
    public async Task RemoveSafeIngredientAsync(Ingredient ingredient)
    {
        var safeIngredient = await _context.SafeIngredients
                                           .FirstOrDefaultAsync(si => si.IngredientId == ingredient.Id);

        if (safeIngredient != null)
        {
            _context.SafeIngredients.Remove(safeIngredient);
            await _context.SaveChangesAsync();
        }
    }


}
}
