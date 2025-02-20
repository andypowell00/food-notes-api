using FoodDiary.Models;

namespace FoodDiary.Services.Abstract
{
    public interface ISafeIngredientService
    {
        public Task<List<SafeIngredient>> GetSafeIngredientsAsync();
        public Task AddSafeIngredientAsync(Ingredient ingredient);
        public Task RemoveSafeIngredientAsync(Ingredient ingredient);
        
    }
}
