using FoodDiary.Data;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.Services
{
    public class UnsafeIngredientService : IUnsafeIngredientService
    {
        private readonly FoodDiaryContext _context;

        public UnsafeIngredientService(FoodDiaryContext context)
        {
            _context = context;
        }
        public async Task<List<UnsafeIngredient>> GetUnsafeIngredientsAsync()
        {
            return await _context.UnsafeIngredients
                                 .Include(si => si.Ingredient)
                                 .ToListAsync();
        }

        public async Task AddUnsafeIngredientAsync(Ingredient ingredient)
        {
            var UnsafeIngredient = new UnsafeIngredient
            {
                IngredientId = ingredient.Id // Associate the UnsafeIngredient with the given Ingredient
            };

            _context.UnsafeIngredients.Add(UnsafeIngredient);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUnsafeIngredientAsync(Ingredient ingredient)
        {
            var UnsafeIngredient = await _context.UnsafeIngredients
                                               .FirstOrDefaultAsync(si => si.IngredientId == ingredient.Id);

            if (UnsafeIngredient != null)
            {
                _context.UnsafeIngredients.Remove(UnsafeIngredient);
                await _context.SaveChangesAsync();
            }
        }


    }
}
