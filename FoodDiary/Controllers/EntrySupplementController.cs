using AutoMapper;
using FoodDiary.DTOs.Create;
using FoodDiary.DTOs;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FoodDiary.Controllers
{
    [ApiController]
    [Route("api/entry-supplements")]
    public class EntrySupplementController : ControllerBase
    {
        private readonly IEntrySupplementService _entrySupplementService;
        private readonly IMapper _mapper;

        public EntrySupplementController(IEntrySupplementService entrySupplementService, IMapper mapper)
        {
            _entrySupplementService = entrySupplementService;
            _mapper = mapper;
        }

        [HttpGet("by-entry/{entryId}")]
        public async Task<IActionResult> GetSupplementsByEntryId(int entryId)
        {
            var entrySupplements = await _entrySupplementService.GetSupplementsByEntryIdAsync(entryId);
            if (entrySupplements == null || !entrySupplements.Any())
                return NoContent();

            var entrySupplementDtos = _mapper.Map<List<EntrySupplementDto>>(entrySupplements);
            return Ok(entrySupplementDtos);
        }

        [HttpGet("{entryId}/{SupplementId}")]
        public async Task<IActionResult> GetEntrySupplementById(int entryId, int SupplementId)
        {
            var entrySupplement = await _entrySupplementService.GetEntrySupplementByIdAsync(entryId, SupplementId);
            if (entrySupplement == null) return NotFound();

            var entrySupplementDto = _mapper.Map<EntrySupplementDto>(entrySupplement);
            return Ok(entrySupplementDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntrySupplement([FromBody] CreateEntrySupplementDto createEntrySupplementDto)
        {
            var entrySupplement = _mapper.Map<EntrySupplement>(createEntrySupplementDto);
            await _entrySupplementService.CreateEntrySupplementAsync(entrySupplement);

            // Reload the entity with navigation properties after creation
            var createdEntrySupplement = await _entrySupplementService.GetEntrySupplementByIdAsync(
                entrySupplement.EntryId, entrySupplement.SupplementId);
            
            if (createdEntrySupplement == null)
                return StatusCode(500, "Created entity could not be retrieved");

            var entrySupplementDto = _mapper.Map<EntrySupplementDto>(createdEntrySupplement);
            return CreatedAtAction(nameof(GetEntrySupplementById),
                new { entryId = entrySupplement.EntryId, supplementId = entrySupplement.SupplementId },
                entrySupplementDto);
        }

        [HttpDelete("{entryId}/{SupplementId}")]
        public async Task<IActionResult> DeleteEntrySupplement(int entryId, int SupplementId)
        {
            var entrySupplement = await _entrySupplementService.GetEntrySupplementByIdAsync(entryId, SupplementId);
            if (entrySupplement == null) return NotFound();

            await _entrySupplementService.DeleteEntrySupplementAsync(entrySupplement);
            return NoContent();
        }
    }
}
