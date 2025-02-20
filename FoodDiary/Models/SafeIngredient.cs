namespace FoodDiary.Models
{
    public class SafeIngredient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
    }
}
