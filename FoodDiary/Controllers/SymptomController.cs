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
    [Route("api/symptoms")]
    public class SymptomController : ControllerBase
    {
        private readonly ISymptomService _symptomService;
        private readonly IMapper _mapper;

        public SymptomController(ISymptomService symptomService, IMapper mapper)
        {
            _symptomService = symptomService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSymptoms()
        {
            var symptoms = await _symptomService.GetAllSymptomsAsync();
            var symptomDtos = _mapper.Map<List<SymptomDto>>(symptoms);
            return Ok(symptomDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSymptomById(int id)
        {
            var symptom = await _symptomService.GetSymptomByIdAsync(id);
            if (symptom == null) return NotFound();

            var symptomDto = _mapper.Map<SymptomDto>(symptom);
            return Ok(symptomDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSymptom([FromBody] CreateSymptomDto createSymptomDto)
        {
            var symptom = _mapper.Map<Symptom>(createSymptomDto);
            await _symptomService.CreateSymptomAsync(symptom);

            var symptomDto = _mapper.Map<SymptomDto>(symptom);
            return CreatedAtAction(nameof(GetSymptomById), new { id = symptomDto.Id }, symptomDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSymptom(int id, [FromBody] UpdateSymptomDto updateSymptomDto)
        {
            if (id != updateSymptomDto.Id) return BadRequest();

            var symptom = await _symptomService.GetSymptomByIdAsync(id);
            if (symptom == null) return NotFound();

            _mapper.Map(updateSymptomDto, symptom);
            await _symptomService.UpdateSymptomAsync(symptom);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSymptom(int id)
        {
            await _symptomService.DeleteSymptomAsync(id);
            return NoContent();
        }
    }
}
