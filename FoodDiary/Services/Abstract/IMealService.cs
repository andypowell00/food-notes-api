using FoodDiary.Models;

namespace FoodDiary.Services.Abstract
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> GetAllMealsAsync();
        Task<Meal?> GetMealByIdAsync(int id);
        Task CreateMealAsync(Meal meal, List<int> ingredientIds);
        Task UpdateMealAsync(Meal meal, List<int> ingredientIds);
        Task DeleteMealAsync(int id);
        Task AddIngredient(int mealId, int ingredientId);
        Task RemoveIngredient(int mealId, int ingredientId);

    }
}
