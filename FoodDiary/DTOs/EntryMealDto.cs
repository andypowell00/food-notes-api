namespace FoodDiary.DTOs
{
    public class EntryMealDto
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public string MealName { get; set; } = string.Empty;
        public List<string> Ingredients { get; set; } = new();

    }
}
