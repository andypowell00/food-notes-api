using System.ComponentModel.DataAnnotations;

namespace FoodDiary.DTOs.Create
{
    public record CreateEntrySupplementDto
    {
        public int EntryId { get; set; }
        public int SupplementId { get; set; }
    }
}
