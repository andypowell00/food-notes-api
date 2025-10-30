namespace FoodDiary.DTOs
{
    public class MealIngredientDto
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
    }
}
