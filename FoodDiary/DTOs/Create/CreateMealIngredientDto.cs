namespace FoodDiary.DTOs.Create
{
    public record CreateMealIngredientDto
    {
        public int MealId { get; set; }
        public int IngredientId { get; set; }
    }
}
