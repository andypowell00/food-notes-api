namespace FoodDiary.DTOs
{
    public record SymptomDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
