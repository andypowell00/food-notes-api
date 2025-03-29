namespace FoodDiary.DTOs
{
    public record SupplementDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
