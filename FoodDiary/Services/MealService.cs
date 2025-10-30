using FoodDiary.Data;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FoodDiary.Services
{
    public class MealService : IMealService
    {
        private readonly FoodDiaryContext _context;

        public MealService(FoodDiaryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            return await _context.Meals
                .Include(m => m.MealIngredients)
                .ThenInclude(mi => mi.Ingredient)
                .ToListAsync();
        }

        public async Task<Meal?> GetMealByIdAsync(int id)
        {
            return await _context.Meals
                .Include(m => m.MealIngredients)
                .ThenInclude(mi => mi.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateMealAsync(Meal meal, List<int> ingredientIds)
        {
            foreach (var ingredientId in ingredientIds)
            {
                meal.MealIngredients.Add(new MealIngredient
                {
                    Meal = meal,
                    IngredientId = ingredientId
                });
            }

            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateMealAsync(Meal meal, List<int> ingredientIds)
        {
            var existingMealIngredients = _context.MealIngredients.Where(mi => mi.MealId == meal.Id);
            _context.MealIngredients.RemoveRange(existingMealIngredients);
            foreach (var ingredientId in ingredientIds)
            {
                meal.MealIngredients.Add(new MealIngredient
                {
                    MealId = meal.Id,
                    IngredientId = ingredientId
                });
            }
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMealAsync(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal != null)
            {
                _context.Meals.Remove(meal);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddIngredient(int mealId, int ingredientId)
        {
            var meal = await _context.Meals
                .Include(m => m.MealIngredients)
                .FirstOrDefaultAsync(m => m.Id == mealId);
            if (meal != null && !meal.MealIngredients.Any(mi => mi.IngredientId == ingredientId))
            {
                meal.MealIngredients.Add(new MealIngredient
                {
                    MealId = mealId,
                    IngredientId = ingredientId
                });
                await _context.SaveChangesAsync();
            }

        }
        public async Task RemoveIngredient(int mealId, int ingredientId)
        {
            var mealIngredient = await _context.MealIngredients
                .FirstOrDefaultAsync(mi => mi.MealId == mealId && mi.IngredientId == ingredientId);

            if (mealIngredient != null)
            {
                _context.MealIngredients.Remove(mealIngredient);
                await _context.SaveChangesAsync();
            }
        }

    }
}
