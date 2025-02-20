namespace FoodDiary.Models
{
    public class UnsafeIngredient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
    }
}
