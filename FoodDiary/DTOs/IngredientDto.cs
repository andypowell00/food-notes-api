using System.ComponentModel.DataAnnotations;

namespace FoodDiary.DTOs
{
    public record IngredientDto
    {
        public int Id { get; set; }
       
        public string Name { get; set; } = string.Empty;
    }
}
