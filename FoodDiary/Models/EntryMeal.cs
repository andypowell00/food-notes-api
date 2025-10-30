namespace FoodDiary.Models
{
    public class EntryMeal
    {
        public int EntryId { get; set; }
        public Entry Entry { get; set; } = null!;

        public int MealId { get; set; }
        public Meal Meal { get; set; } = null!;

    }
}
