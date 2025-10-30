using AutoMapper;
using FoodDiary.DTOs;
using FoodDiary.DTOs.Create;
using FoodDiary.DTOs.Update;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.AspNetCore.Mvc;


namespace FoodDiary.Controllers
{
    [ApiController]
    [Route("api/entrymeals")]
    public class EntryMealController : ControllerBase
    {

        private readonly IEntryMealService _entryMealService;
        private readonly IMapper _mapper;

        public EntryMealController(IEntryMealService entryMealService, IMapper mapper)
        {
            _entryMealService = entryMealService;
            _mapper = mapper;
        }
        [HttpGet("entry/{entryId}")]
        public async Task<IActionResult> GetMealsByEntryId(int entryId)
        {

            var entryMeals = await _entryMealService.GetMealsByEntryIdAsync(entryId);

            var result = entryMeals.Select(em => new EntryMealDto
            {
                MealId = em.MealId,
                MealName = em.Meal.Name,
                Ingredients = em.Meal.MealIngredients.Select(mi => mi.Ingredient.Name).ToList()
            }).ToList();

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEntryMeal([FromBody] CreateEntryMealDto createEntryMealDto)
        {
            var entryMeal = _mapper.Map<EntryMeal>(createEntryMealDto);
            await _entryMealService.CreateEntryMealAsync(entryMeal);
            var entryMealDto = _mapper.Map<EntryMealDto>(entryMeal);
            return CreatedAtAction(nameof(GetMealsByEntryId), new { entryId = entryMealDto.Id }, entryMealDto);
        }
        [HttpDelete("entry/{entryId}/meal/{mealId}")]
        public async Task<IActionResult> DeleteEntryMeal(int entryId, int mealId)
        {
            var entryMeal = await _entryMealService.GetEntryMealByIdAsync(entryId, mealId);
            if (entryMeal == null) return NotFound();
            await _entryMealService.DeleteEntryMealAsync(entryMeal);
            return NoContent();
        }
        [HttpGet("entry/{entryId}/meal/{mealId}")]
        public async Task<IActionResult> GetEntryMealById(int entryId, int mealId)
        {
            var entryMeal = await _entryMealService.GetEntryMealByIdAsync(entryId, mealId);
            if (entryMeal == null) return NotFound();
            var entryMealDto = _mapper.Map<EntryMealDto>(entryMeal);
            return Ok(entryMealDto);
        }




    }
}
