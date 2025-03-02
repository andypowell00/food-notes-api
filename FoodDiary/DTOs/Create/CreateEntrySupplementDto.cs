using System.ComponentModel.DataAnnotations;

namespace FoodDiary.DTOs.Create
{
    public class CreateEntrySupplementDto
    {
        public int EntryId { get; set; }
        public int SupplementId { get; set; }
    }
}
