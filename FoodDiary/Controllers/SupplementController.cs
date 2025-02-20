using AutoMapper;
using FoodDiary.DTOs.Create;
using FoodDiary.DTOs;
using FoodDiary.Models;
using FoodDiary.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FoodDiary.Controllers
{
    [ApiController]
    [Route("api/supplements")]
    public class SupplementController : ControllerBase
    {
        private readonly ISupplementService _supplementService;
        private readonly IMapper _mapper;

        public SupplementController(ISupplementService SupplementService, IMapper mapper)
        {
            _supplementService = SupplementService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSupplements()
        {
            var Supplements = await _supplementService.GetAllSupplementsAsync();
            var SupplementDtos = _mapper.Map<List<SupplementDto>>(Supplements);
            return Ok(SupplementDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplementById(int id)
        {
            var Supplement = await _supplementService.GetSupplementByIdAsync(id);
            if (Supplement == null) return NotFound();

            var SupplementDto = _mapper.Map<SupplementDto>(Supplement);
            return Ok(SupplementDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplement([FromBody] CreateSupplementDto createSupplementDto)
        {
            var Supplement = _mapper.Map<Supplement>(createSupplementDto);
            await _supplementService.CreateSupplementAsync(Supplement);

            var SupplementDto = _mapper.Map<SupplementDto>(Supplement);
            return CreatedAtAction(nameof(GetSupplementById), new { id = SupplementDto.Id }, SupplementDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplement(int id)
        {
            await _supplementService.DeleteSupplementAsync(id);
            return NoContent();
        }
    }
}
