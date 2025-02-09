namespace FoodDiary.DTOs
{
    public class EntryDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool Symptomatic { get; set; }
        public List<SymptomDto> Symptoms { get; set; } = new();
        public List<IngredientDto> Ingredients { get; set; } = new();
    }
}