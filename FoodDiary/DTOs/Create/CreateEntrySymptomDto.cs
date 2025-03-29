namespace FoodDiary.DTOs.Create
{
    public record CreateEntrySymptomDto
    {
        public int EntryId { get; set; }
        public int SymptomId { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
