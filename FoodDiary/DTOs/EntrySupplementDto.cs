namespace FoodDiary.DTOs
{
    public record EntrySupplementDto(
        int EntryId,
        int SupplementId,
        string SupplementName = "");
}
