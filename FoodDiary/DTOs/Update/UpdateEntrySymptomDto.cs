using System.ComponentModel.DataAnnotations;

namespace FoodDiary.DTOs.Update
{
    public class UpdateEntrySymptomDto
    {
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Notes must be between 1 and 500 characters.")]
        public string Notes { get; set; } = string.Empty;
    }
}

