using FoodDiary.DTOs.Create;

namespace FoodDiary.DTOs.Update
{
    public class UpdateEntryDto : CreateEntryDto
    {
        public int Id { get; set; }
    }

}