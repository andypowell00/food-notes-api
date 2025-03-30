namespace FoodDiary.DTOs
{
    public record EntrySymptomDto(
        int EntryId,
        int SymptomId,
        string SymptomTitle = "", // Optional
        string Notes = "");
}
