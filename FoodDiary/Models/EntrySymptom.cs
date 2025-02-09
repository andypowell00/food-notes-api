namespace FoodDiary.Models
{
    public class EntrySymptom
    {
        public int EntryId { get; set; }
        public Entry Entry { get; set; } = null!;

        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; } = null!;

        public string Notes { get; set; } = string.Empty;
    }
}
