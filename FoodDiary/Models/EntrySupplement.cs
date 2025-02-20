namespace FoodDiary.Models
{
    public class EntrySupplement
    {
        public int EntryId { get; set; }
        public Entry Entry { get; set; } = null!;

        public int SupplementId { get; set; }
        public Supplement Supplement { get; set; } = null!;
    }
}
