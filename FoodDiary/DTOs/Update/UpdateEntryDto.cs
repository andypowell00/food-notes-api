using FoodDiary.DTOs.Create;

namespace FoodDiary.DTOs.Update
{
    public record UpdateEntryDto(int Id) : CreateEntryDto
    {
    }
}