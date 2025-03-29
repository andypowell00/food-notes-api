namespace FoodDiary.DTOs
{
    public record EntrySymptomDto
    {
        public int EntryId { get; set; }
        public int SymptomId { get; set; }
        public string SymptomTitle { get; set; } = string.Empty; // Optional

        public string Notes { get; set; } = string.Empty;
    }
}
