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
    [Route("api/entry-symptoms")]
    public class EntrySymptomController : ControllerBase
    {
        private readonly IEntrySymptomService _entrySymptomService;
        private readonly IMapper _mapper;

        public EntrySymptomController(IEntrySymptomService entrySymptomService, IMapper mapper)
        {
            _entrySymptomService = entrySymptomService;
            _mapper = mapper;
        }

        [HttpGet("by-entry/{entryId}")]
        public async Task<IActionResult> GetSymptomsByEntryId(int entryId)
        {
            var entrySymptoms = await _entrySymptomService.GetSymptomsByEntryIdAsync(entryId);
            if (entrySymptoms == null || !entrySymptoms.Any())
                return NoContent();

            var entrySymptomDtos = _mapper.Map<List<EntrySymptomDto>>(entrySymptoms);
            return Ok(entrySymptomDtos);
        }

        [HttpGet("{entryId}/{symptomId}")]
        public async Task<IActionResult> GetEntrySymptomById(int entryId, int symptomId)
        {
            var entrySymptom = await _entrySymptomService.GetEntrySymptomByIdAsync(entryId, symptomId);
            if (entrySymptom == null) return NotFound();

            var entrySymptomDto = _mapper.Map<EntrySymptomDto>(entrySymptom);
            return Ok(entrySymptomDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntrySymptom([FromBody] CreateEntrySymptomDto createEntrySymptomDto)
        {
            var entrySymptom = _mapper.Map<EntrySymptom>(createEntrySymptomDto);
            await _entrySymptomService.CreateEntrySymptomAsync(entrySymptom);

            var entrySymptomDto = _mapper.Map<EntrySymptomDto>(entrySymptom);
            return CreatedAtAction(nameof(GetEntrySymptomById),
                new { entryId = entrySymptom.EntryId, symptomId = entrySymptom.SymptomId },
                entrySymptomDto);
        }

        [HttpPut("{entryId}/{symptomId}")]
        public async Task<IActionResult> UpdateEntrySymptom(int entryId, int symptomId, [FromBody] UpdateEntrySymptomDto updateEntrySymptomDto)
        {
    
            var entrySymptom = await _entrySymptomService.GetEntrySymptomByIdAsync(entryId, symptomId);
            if (entrySymptom == null) return NotFound();

            entrySymptom.Notes = updateEntrySymptomDto.Notes;

            
            await _entrySymptomService.UpdateEntrySymptomAsync(entrySymptom);
            return NoContent();
        }

        [HttpDelete("{entryId}/{symptomId}")]
        public async Task<IActionResult> DeleteEntrySymptom(int entryId, int symptomId)
        {
            var entrySymptom = await _entrySymptomService.GetEntrySymptomByIdAsync(entryId, symptomId);
            if (entrySymptom == null) return NotFound();

            await _entrySymptomService.DeleteEntrySymptomAsync(entrySymptom);
            return NoContent();
        }
    }
}
