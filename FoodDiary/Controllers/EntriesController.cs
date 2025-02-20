using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FoodDiary.DTOs;
using FoodDiary.DTOs.Create;
using FoodDiary.DTOs.Update;

namespace FoodDiary.Controllers
{
    [ApiController]
    [Route("api/entries")]
    public class EntriesController : ControllerBase
    {
        private readonly IEntryService _entryService;
        private readonly IMapper _mapper;

        public EntriesController(IEntryService entryService, IMapper mapper)
        {
            _entryService = entryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEntries()
        {
            var entries = await _entryService.GetAllEntriesAsync();
            var entryDtos = _mapper.Map<List<EntryDto>>(entries);
            return Ok(entryDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntryById(int id)
        {
            var entry = await _entryService.GetEntryByIdAsync(id);
            if (entry == null) return NotFound();

            var entryDto = _mapper.Map<EntryDto>(entry);
            return Ok(entryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryDto createEntryDto)
        {
            var entry = _mapper.Map<Entry>(createEntryDto);

            // Handle additional logic for symptoms and ingredients
            entry.EntrySymptoms = createEntryDto.SymptomIds.Select(id => new EntrySymptom
            {
                SymptomId = id
            }).ToList();

            entry.EntryIngredients = createEntryDto.Ingredients.Select(i => new EntryIngredient
            {
                IngredientId = i.IngredientId,
                Notes = i.Notes
            }).ToList();

            await _entryService.CreateEntryAsync(entry);

            var entryDto = _mapper.Map<EntryDto>(entry);
            return CreatedAtAction(nameof(GetEntryById), new { id = entryDto.Id }, entryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntry(int id, [FromBody] UpdateEntryDto updateEntryDto)
        {
            if (id != updateEntryDto.Id) return BadRequest();

            var entry = await _entryService.GetEntryByIdAsync(id);
            if (entry == null) return NotFound();

            // Update entry fields
            entry.Date = updateEntryDto.Date;
            entry.Symptomatic = updateEntryDto.Symptomatic;

            // Update relationships (optional logic for symptoms/ingredients can be added here)
            entry.EntrySymptoms = updateEntryDto.SymptomIds.Select(id => new EntrySymptom
            {
                EntryId = entry.Id,
                SymptomId = id
            }).ToList();

            entry.EntryIngredients = updateEntryDto.Ingredients.Select(i => new EntryIngredient
            {
                EntryId = entry.Id,
                IngredientId = i.IngredientId,
                Notes = i.Notes
            }).ToList();

            await _entryService.UpdateEntryAsync(entry);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            await _entryService.DeleteEntryAsync(id);
            return NoContent();
        }

        [HttpGet("symptomatic")]
        public async Task<IActionResult> GetSymptomaticEntries()
        {
            var entries = await _entryService.GetSymptomaticEntriesAsync();
            var entryDtos = _mapper.Map<List<EntryDto>>(entries);
            return Ok(entryDtos);
        }
    }
}
