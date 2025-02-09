using FoodDiary.Models;

namespace FoodDiary.Services.Abstract
{
    public interface IEntryIngredientService
    {
        Task<List<EntryIngredient>> GetIngredientsByEntryIdAsync(int entryId);
        Task<EntryIngredient?> GetEntryIngredientByIdAsync(int entryId, int symptomId);
        Task CreateEntryIngredientAsync(EntryIngredient EntryIngredient);
        Task UpdateEntryIngredientAsync(EntryIngredient EntryIngredient);
        Task DeleteEntryIngredientAsync(EntryIngredient EntryIngredient);
    }
}
