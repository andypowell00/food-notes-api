using FoodDiary.Models;

namespace FoodDiary.Services.Abstract
{
    public interface ISymptomService
    {
        Task<IEnumerable<Symptom>> GetAllSymptomsAsync();
        Task<Symptom?> GetSymptomByIdAsync(int id);
        Task CreateSymptomAsync(Symptom Symptom);
        Task UpdateSymptomAsync(Symptom Symptom);
        Task DeleteSymptomAsync(int id);
    }
}
