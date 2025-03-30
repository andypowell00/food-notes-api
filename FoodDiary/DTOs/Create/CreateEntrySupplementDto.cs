using System.ComponentModel.DataAnnotations;

namespace FoodDiary.DTOs.Create
{
    public record CreateEntrySupplementDto(
        int EntryId,
        int SupplementId);
}
