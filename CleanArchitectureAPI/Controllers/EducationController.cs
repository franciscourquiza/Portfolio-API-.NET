using Application.Dtos.EducationDtos;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _service;
        private readonly IValidator<EducationForAddDto> _validator;
        private readonly IValidator<EducationForEditDto> _validatorForEdit;
        public EducationController(IEducationService service, IValidator<EducationForAddDto> validator, IValidator<EducationForEditDto> validatorForEdit)
        {
            _service = service;
            _validator = validator; 
            _validatorForEdit = validatorForEdit;
        }
        [HttpGet("GetByTitle/{title}", Name ="GetEducationByTitle")]
        public async Task<IActionResult> GetEducationByTitle([FromRoute] string title)
        {
            Education? education = await _service.GetByTitle(title);
            if (education == null)
            {
                return NotFound("Educación no encontrada.");
            }
            return Ok(education);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetEducationById([FromRoute] int id)
        {
            var education = await _service.GetById(id);
            if (education == null) { return NotFound("Educación no encontrada."); }
            return Ok(education);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.Get());
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddEducation([FromBody] EducationForAddDto body)
        {
            var validationResult = _validator.Validate(body);
            if (!validationResult.IsValid) 
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            string userEmail = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            Education createdEducation = await _service.Add(body, userEmail);

            return CreatedAtRoute(nameof(GetEducationByTitle), new { title = createdEducation.Title }, createdEducation);
        }

        [HttpPut("UpdateByTitle/{title}")]
        public async Task<IActionResult> UpdateEducation([FromRoute] string title, [FromBody] EducationForEditDto body)
        {
            var validationResult = _validatorForEdit.Validate(body);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            string? userEmail = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
            if (body == null) { return BadRequest(); }
            return Ok(await _service.Update(body, title, userEmail));
        }

        [HttpDelete("DeleteByTitle/{title}")]
        public async Task<IActionResult> Delete([FromRoute] string title)
        {
            string? userEmail = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
            await _service.Delete(title, userEmail);
            return Ok("Educación eliminada correctamente.");
        }
    }
}
