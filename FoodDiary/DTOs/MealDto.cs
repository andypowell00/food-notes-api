namespace FoodDiary.DTOs
{
    public record MealDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<IngredientDto> Ingredients { get; set; } = new();
    }
}
