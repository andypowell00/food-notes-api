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
    [Route("api/meals")]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;
        private readonly IMapper _mapper;

        public MealController(IMealService mealService, IMapper mapper)
        {
            _mealService = mealService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllmeals()
        {
            var meals = await _mealService.GetAllMealsAsync();
            var mealDtos = _mapper.Map<List<MealDto>>(meals);
            return Ok(mealDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetmealById(int id)
        {
            var meal = await _mealService.GetMealByIdAsync(id);
            if (meal == null) return NotFound();

            var mealDto = _mapper.Map<MealDto>(meal);
            return Ok(mealDto);
        }

        [HttpPost]
        public async Task<IActionResult> Createmeal([FromBody] CreateMealDto createmealDto)
        {
            var meal = _mapper.Map<Meal>(createmealDto);
            await _mealService.CreateMealAsync(meal, createmealDto.IngredientIds);

            var mealDto = _mapper.Map<MealDto>(meal);
            return CreatedAtAction(nameof(GetmealById), new { id = mealDto.Id }, mealDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updatemeal(int id, [FromBody] UpdateMealDto updatemealDto)
        {
            if (id != updatemealDto.Id) return BadRequest();

            var meal = await _mealService.GetMealByIdAsync(id);
            if (meal == null) return NotFound();

            _mapper.Map(updatemealDto, meal);
            await _mealService.UpdateMealAsync(meal, updatemealDto.IngredientIds);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletemeal(int id)
        {
            await _mealService.DeleteMealAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/ingredients")]
        public async Task<IActionResult> AddIngredientToMeal(int id, [FromBody] int ingredientId)
        {
            await _mealService.AddIngredient(id, ingredientId);
            return NoContent();
        }



        [HttpDelete("{id}/ingredients/{ingredientId}")]
        public async Task<IActionResult> RemoveIngredientFromMeal(int id, int ingredientId)
        {
            await _mealService.RemoveIngredient(id, ingredientId);
            return NoContent();
        }


    }
}
