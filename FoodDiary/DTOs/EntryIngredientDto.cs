namespace FoodDiary.DTOs
{
    public class EntryIngredientDto
    {
        public int EntryId { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty; 
       
        public string Notes { get; set; } = string.Empty;
    }

}
