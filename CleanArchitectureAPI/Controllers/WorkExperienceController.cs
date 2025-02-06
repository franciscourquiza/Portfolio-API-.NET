using Application.Dtos.WorkExperienceDtos;
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
    public class WorkExperienceController : ControllerBase
    {
        private readonly IWorkExperienceService _service;
        private readonly IValidator<WorkExperienceForAdd> _validator;
        private readonly IValidator<WorkExperienceForEditDto> _validatorForEdit;
        public WorkExperienceController(IWorkExperienceService service, IValidator<WorkExperienceForAdd> validator, IValidator<WorkExperienceForEditDto> validatorForEdit)
        {
            _service = service;
            _validator = validator;
            _validatorForEdit = validatorForEdit;
        }

        [HttpGet("GetByTitle/{title}", Name = "GetWorkExperienceByTitle")]
        public async Task<IActionResult> GetWorkExperienceByTitle([FromRoute]string title)
        {
            WorkExperience? workExperience = await _service.Get(title);
            if (workExperience == null)
            {
                return NotFound("Experiencia laboral no encontrada");
            }
            return Ok(workExperience);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetWorkExperienceById([FromRoute]int id) 
        {
            WorkExperience? workExperience = await _service.Get(id);
            if (workExperience == null)
            {
                return NotFound("Experiencia laboral no encontrada");
            }
            return Ok(workExperience);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() 
        {
            return Ok(await _service.Get());
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddWorkExperience([FromBody]WorkExperienceForAdd request) 
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            string userEmail = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            WorkExperience? createdWorkExperience = await _service.AddWorkExperience(request, userEmail);

            return CreatedAtRoute(nameof(GetWorkExperienceByTitle), new { title = createdWorkExperience?.Title }, createdWorkExperience);
        }

        [HttpPut("UpdateByTitle/{title}")]
        public async Task<IActionResult> UpdateWorkExperience([FromRoute] string title, [FromBody] WorkExperienceForEditDto body)
        {
            var validationResult = await _validatorForEdit.ValidateAsync(body);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            string? userEmail = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
            if (body == null) { return BadRequest(); }
            await _service.Update(body, title, userEmail);
            return Ok(body);

        }

        [HttpDelete("DeleteByTitle/{title}")]
        public async Task<IActionResult> Delete([FromRoute] string title) 
        {
            string? userEmail = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
            await _service.Delete(title, userEmail);
            return Ok("La experiencia laboral fue eliminada correctamente.");
        }
    }
}
