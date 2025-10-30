using FoodDiary.Models;

namespace FoodDiary.Services.Abstract
{
    public interface IEntryMealService
    {
        // Get all meals for a specific entry
        Task<List<EntryMeal>> GetMealsByEntryIdAsync(int entryId);

        // Get a single EntryMeal by entryId and mealId
        Task<EntryMeal?> GetEntryMealByIdAsync(int entryId, int mealId);

        // Add a meal to an entry
        Task CreateEntryMealAsync(EntryMeal entryMeal);

        // Remove a meal from an entry
        Task DeleteEntryMealAsync(EntryMeal entryMeal);

    }
}
