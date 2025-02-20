using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using FoodDiary.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FoodDiary.DTOs.Create;
using FoodDiary.DTOs.Update;

namespace FoodDiary.Controllers
{
    [ApiController]
    [Route("api/ingredients")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        private readonly IMapper _mapper;

        public IngredientController(IIngredientService ingredientService, IMapper mapper)
        {
            _ingredientService = ingredientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            var ingredientDtos = _mapper.Map<List<IngredientDto>>(ingredients);
            return Ok(ingredientDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null) return NotFound();

            var ingredientDto = _mapper.Map<IngredientDto>(ingredient);
            return Ok(ingredientDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientDto createIngredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(createIngredientDto);
            await _ingredientService.CreateIngredientAsync(ingredient);

            var ingredientDto = _mapper.Map<IngredientDto>(ingredient);
            return CreatedAtAction(nameof(GetIngredientById), new { id = ingredientDto.Id }, ingredientDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] UpdateIngredientDto updateIngredientDto)
        {
            if (id != updateIngredientDto.Id) return BadRequest();

            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null) return NotFound();

            _mapper.Map(updateIngredientDto, ingredient);
            await _ingredientService.UpdateIngredientAsync(ingredient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            await _ingredientService.DeleteIngredientAsync(id);
            return NoContent();
        }
    }
}
