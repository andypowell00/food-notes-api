namespace FoodDiary.DTOs.Create
{
    public record CreateEntrySymptomDto(
        int EntryId,
        int SymptomId,
        string Notes = "");
}
