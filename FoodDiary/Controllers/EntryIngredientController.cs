using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FoodDiary.DTOs;

namespace FoodDiary.Controllers
{
    [ApiController]
    [Route("api/entry-ingredients")]
    public class EntryIngredientController : ControllerBase
    {
        private readonly IEntryIngredientService _entryIngredientService;
        private readonly IMapper _mapper;

        public EntryIngredientController(IEntryIngredientService entryIngredientService, IMapper mapper)
        {
            _entryIngredientService = entryIngredientService;
            _mapper = mapper;
        }

        [HttpGet("by-entry/{entryId}")]
        public async Task<IActionResult> GetIngredientsByEntryId(int entryId)
        {
            var entryIngredients = await _entryIngredientService.GetIngredientsByEntryIdAsync(entryId);
            if (entryIngredients == null || !entryIngredients.Any())
                return NoContent();

            var entryIngredientDtos = _mapper.Map<List<EntryIngredientDto>>(entryIngredients);
            return Ok(entryIngredientDtos);
        }

        [HttpGet("{entryId}/{ingredientId}")]
        public async Task<IActionResult> GetEntryIngredientById(int entryId, int ingredientId)
        {
            var entryIngredient = await _entryIngredientService.GetEntryIngredientByIdAsync(entryId, ingredientId);
            if (entryIngredient == null) return NotFound();

            var entryIngredientDto = _mapper.Map<EntryIngredientDto>(entryIngredient);
            return Ok(entryIngredientDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntryIngredient([FromBody] CreateEntryIngredientDto createEntryIngredientDto)
        {
            var entryIngredient = _mapper.Map<EntryIngredient>(createEntryIngredientDto);
            await _entryIngredientService.CreateEntryIngredientAsync(entryIngredient);

            var entryIngredientDto = _mapper.Map<EntryIngredientDto>(entryIngredient);
            return CreatedAtAction(nameof(GetEntryIngredientById),
                new { entryId = entryIngredient.EntryId, ingredientId = entryIngredient.IngredientId },
                entryIngredientDto);
        }

        [HttpPut("{entryId}/{ingredientId}")]
        public async Task<IActionResult> UpdateEntryIngredient(int entryId, int ingredientId, [FromBody] UpdateEntryIngredientDto updateEntryIngredientDto)
        {

            var entryIngredient = await _entryIngredientService.GetEntryIngredientByIdAsync(entryId, ingredientId);
            if (entryIngredient == null) return NotFound();

            
            entryIngredient.Notes = updateEntryIngredientDto.Notes;

            await _entryIngredientService.UpdateEntryIngredientAsync(entryIngredient);
            return NoContent();
        }


        [HttpDelete("{entryId}/{ingredientId}")]
        public async Task<IActionResult> DeleteEntryIngredient(int entryId, int ingredientId)
        {
            var entryIngredient = await _entryIngredientService.GetEntryIngredientByIdAsync(entryId, ingredientId);
            if (entryIngredient == null) return NotFound();

            await _entryIngredientService.DeleteEntryIngredientAsync(entryIngredient);
            return NoContent();
        }
    }
}
