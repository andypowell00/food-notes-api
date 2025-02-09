using FoodDiary.Models;

namespace FoodDiary.Services.Abstract
{
    public interface IEntrySymptomService
    {
        Task<List<EntrySymptom>> GetSymptomsByEntryIdAsync(int entryId);
        Task<EntrySymptom?> GetEntrySymptomByIdAsync(int entryId, int symptomId);
        Task CreateEntrySymptomAsync(EntrySymptom EntrySymptom);
        Task UpdateEntrySymptomAsync(EntrySymptom EntrySymptom);
        Task DeleteEntrySymptomAsync(EntrySymptom entrySymptom);
    }
}
