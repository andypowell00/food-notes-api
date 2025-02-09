namespace FoodDiary.Models
{
    public class EntryIngredient
    {
        public int EntryId { get; set; }
        public Entry Entry { get; set; } = null!;

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;

        public string Notes { get; set; } = string.Empty;
    }
}
