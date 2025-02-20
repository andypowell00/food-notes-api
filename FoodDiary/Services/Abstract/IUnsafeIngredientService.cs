using FoodDiary.Models;

namespace FoodDiary.Services.Abstract
{
    public interface IUnsafeIngredientService
    {
        public Task<List<UnsafeIngredient>> GetUnsafeIngredientsAsync();
        public Task AddUnsafeIngredientAsync(Ingredient ingredient);
        public Task RemoveUnsafeIngredientAsync(Ingredient ingredient);
    }
}
