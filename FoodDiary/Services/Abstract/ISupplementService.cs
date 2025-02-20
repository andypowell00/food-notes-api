using FoodDiary.Models;

namespace FoodDiary.Services.Abstract
{
    public interface ISupplementService
    {
        Task<IEnumerable<Supplement>> GetAllSupplementsAsync();
        Task<Supplement?> GetSupplementByIdAsync(int id);
        Task CreateSupplementAsync(Supplement Supplement);
        Task DeleteSupplementAsync(int id);
    }
}
