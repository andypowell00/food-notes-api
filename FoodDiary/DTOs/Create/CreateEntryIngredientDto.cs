namespace FoodDiary.DTOs.Create
{
    public record CreateEntryIngredientDto
    {
        public int EntryId { get; set; }
        public int IngredientId { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
