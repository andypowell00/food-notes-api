using AutoMapper;
using FoodDiary.DTOs.Create;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FoodDiary.Controllers
{
    [ApiController]
    [Route("api/unsafe-ingredients")]
    public class UnsafeIngredientController : ControllerBase
    {
        private readonly IUnsafeIngredientService _UnsafeIngredientService;
        private readonly IMapper _mapper;

        public UnsafeIngredientController(IUnsafeIngredientService UnsafeIngredientService, IMapper mapper)
        {
            _UnsafeIngredientService = UnsafeIngredientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUnsafeIngredients()
        {
            var UnsafeIngredients = await _UnsafeIngredientService.GetUnsafeIngredientsAsync();
            if (UnsafeIngredients == null || !UnsafeIngredients.Any())
                return NoContent();

            
            return Ok(UnsafeIngredients);
        }

        [HttpPost]
        public async Task<IActionResult> AddUnsafeIngredient([FromBody] CreateUnsafeIngredientDto createUnsafeIngredientDto)
        {
            var UnsafeIngredient = _mapper.Map<UnsafeIngredient>(createUnsafeIngredientDto);
            await _UnsafeIngredientService.AddUnsafeIngredientAsync(new Ingredient { Id = createUnsafeIngredientDto.IngredientId });

            
            return CreatedAtAction(nameof(AddUnsafeIngredient), new { id = UnsafeIngredient.Id }, UnsafeIngredient);
        }

        [HttpDelete("{ingredientId}")]
        public async Task<IActionResult> RemoveUnsafeIngredient(int ingredientId)
        {
            await _UnsafeIngredientService.RemoveUnsafeIngredientAsync(new Ingredient { Id = ingredientId });
            return NoContent();
        }
    }
}
