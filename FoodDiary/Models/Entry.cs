namespace FoodDiary.Models
{
    public class Entry
    {
        public int Id { get; set; } 
        public DateTime Date { get; set; } 
        public bool Symptomatic { get; set; } 

        public ICollection<EntrySymptom> EntrySymptoms { get; set; } = new List<EntrySymptom>();
        public ICollection<EntryIngredient> EntryIngredients { get; set; } = new List<EntryIngredient>();
        public ICollection<EntrySupplement> EntrySupplements { get; set; } = new List<EntrySupplement>();
        public ICollection<EntryMeal> EntryMeals { get; set; } = new List<EntryMeal>(); 
    }
}
