using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FoodDiary.DTOs;
using FoodDiary.DTOs.Create;
using FoodDiary.DTOs.Update;
using FoodDiary.Services;

namespace FoodDiary.Controllers
{
    [ApiController]
    [Route("api/safe-ingredients")]
    public class SafeIngredientController : ControllerBase
    {
        private readonly ISafeIngredientService _safeIngredientService;
        private readonly IMapper _mapper;

        public SafeIngredientController(ISafeIngredientService safeIngredientService, IMapper mapper)
        {
            _safeIngredientService = safeIngredientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSafeIngredients()
        {
            var SafeIngredients = await _safeIngredientService.GetSafeIngredientsAsync();
            if (SafeIngredients == null || !SafeIngredients.Any())
                return NoContent();

            
            return Ok(SafeIngredients);
        }

        [HttpPost]
        public async Task<IActionResult> AddSafeIngredient([FromBody] CreateSafeIngredientDto createSafeIngredientDto)
        {
            var SafeIngredient = _mapper.Map<SafeIngredient>(createSafeIngredientDto);
            await _safeIngredientService.AddSafeIngredientAsync(new Ingredient { Id = createSafeIngredientDto.IngredientId });


            return CreatedAtAction(nameof(AddSafeIngredient), new { id = SafeIngredient.Id }, SafeIngredient);
        }

        [HttpDelete("{ingredientId}")]
        public async Task<IActionResult> RemoveSafeIngredient(int ingredientId)
        {
            await _safeIngredientService.RemoveSafeIngredientAsync(new Ingredient { Id = ingredientId });
            return NoContent();
        }
    }
}
