namespace FoodDiary.DTOs.Create
{
    public record CreateMealDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<int> IngredientIds { get; set; } = new();
    }
}
