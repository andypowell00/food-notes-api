namespace FoodDiary.DTOs.Update
{
    public record UpdateMealDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<int> IngredientIds { get; set; } = new();
    }
}
