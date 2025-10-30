namespace FoodDiary.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<EntryMeal> EntryMeals { get; set; } = new List<EntryMeal>();
        public ICollection<MealIngredient> MealIngredients { get; set; } = new List<MealIngredient>();
    }
}
