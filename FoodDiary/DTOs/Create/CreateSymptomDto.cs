using System.ComponentModel.DataAnnotations;

namespace FoodDiary.DTOs.Create
{
    public record CreateSymptomDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Title { get; set; } = string.Empty;
    }
}
