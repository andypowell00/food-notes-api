namespace FoodDiary.DTOs.Create
{
    public record CreateEntryDto
    {
        public DateTime Date { get; set; }
        public bool Symptomatic { get; set; }
        public List<int> SymptomIds { get; set; } = new();
        public List<EntryIngredientDto> Ingredients { get; set; } = new();
    }

}