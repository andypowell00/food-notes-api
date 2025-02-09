using FoodDiary.Models;

namespace FoodDiary.Services.Abstract
{
    public interface IEntryService
    {
        Task<IEnumerable<Entry>> GetAllEntriesAsync();
        Task<Entry?> GetEntryByIdAsync(int id);
        Task CreateEntryAsync(Entry Entry);
        Task UpdateEntryAsync(Entry Entry);
        Task DeleteEntryAsync(int id);
        Task<IEnumerable<Entry>> GetSymptomaticEntriesAsync();
    }
}
