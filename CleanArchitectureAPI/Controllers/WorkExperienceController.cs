using Application.Dtos.WorkExperienceDtos;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkExperienceController : ControllerBase
    {
        private readonly WorkExperienceService _service;
        public WorkExperienceController(WorkExperienceService service)
        {
            _service = service;
        }

        [HttpGet("GetByTitle/{title}", Name = "GetWorkExperienceByTitle")]
        public IActionResult GetWorkExperienceByTitle([FromRoute]string title)
        {
            var workExperience = _service.Get(title);
            if (workExperience == null)
            {
                return NotFound("Experiencia laboral no encontrada");
            }
            return Ok(workExperience);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult Get([FromRoute]int id) 
        {
            var workExperience = _service.Get(id);
            if (workExperience == null)
            {
                return NotFound("Experiencia laboral no encontrada");
            }
            return Ok(workExperience);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll() 
        {
            return Ok(_service.Get());
        }

        [HttpPost("Create")]
        public IActionResult AddWorkExperience([FromBody]WorkExperienceForAdd request) 
        {
            string userEmail = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            WorkExperience createdWorkExperience = _service.AddWorkExperience(request, userEmail);

            return CreatedAtRoute(nameof(GetWorkExperienceByTitle), new { title = createdWorkExperience.Title }, createdWorkExperience);
        }

        [HttpPut("UpdateByTitle/{title}")]
        public IActionResult UpdateWorkExperience([FromRoute] string title, [FromBody] WorkExperienceForEditDto body)
        {
            string? userEmail = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
            if (body == null) { return BadRequest(); }
            _service.Update(body, title, userEmail);
            return Ok(body);

        }

        [HttpDelete("DeleteByTitle/{title}")]
        public IActionResult Delete([FromRoute] string title) 
        {
            string? userEmail = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
            _service.Delete(title, userEmail);
            return Ok("La experiencia laboral fue eliminada correctamente.");
        }
    }
}
