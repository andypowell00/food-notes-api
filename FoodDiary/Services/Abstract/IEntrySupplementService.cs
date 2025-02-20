using FoodDiary.Models;

namespace FoodDiary.Services.Abstract
{
    public interface IEntrySupplementService
    {
        Task<List<EntrySupplement>> GetSupplementsByEntryIdAsync(int entryId);
        Task<EntrySupplement?> GetEntrySupplementByIdAsync(int entryId, int symptomId);
        Task CreateEntrySupplementAsync(EntrySupplement EntrySupplement);
        Task DeleteEntrySupplementAsync(EntrySupplement EntrySupplement);
    }
}
