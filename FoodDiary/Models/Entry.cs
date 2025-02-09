namespace FoodDiary.Models
{
    public class Entry
    {
        public int Id { get; set; } 
        public DateTime Date { get; set; } 
        public bool Symptomatic { get; set; } 

        public ICollection<EntrySymptom> EntrySymptoms { get; set; } = new List<EntrySymptom>();
        public ICollection<EntryIngredient> EntryIngredients { get; set; } = new List<EntryIngredient>();
    }
}
