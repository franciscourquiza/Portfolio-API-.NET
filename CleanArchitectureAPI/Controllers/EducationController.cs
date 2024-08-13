using Application.Dtos.EducationDtos;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EducationController : ControllerBase
    {
        private readonly EducationService _service;
        public EducationController(EducationService service)
        {
            _service = service;
        }
        [HttpGet("GetByTitle/{title}", Name ="GetEducationByTitle")]
        public IActionResult GetEducationByTitle([FromRoute] string title)
        {
            Education? education = _service.GetByTitle(title);
            if (education == null)
            {
                return NotFound("Educación no encontrada");
            }
            return Ok(education);
        }
        [HttpGet("GetById/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var education = _service.Get(id);
            if (education == null) { return NotFound("Educación no encontrada"); }
            return Ok(education);
        }

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(_service.Get());
        }

        [HttpPost("Create")]
        public IActionResult Add([FromBody] EducationForAddDto body)
        {
            string userEmail = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            Education createdEducation = _service.Add(body, userEmail);

            return CreatedAtRoute(nameof(GetEducationByTitle), new { title = createdEducation.Title }, createdEducation);
        }

        [HttpPut("UpdateByTitle/{title}")]
        public IActionResult UpdateEducation([FromRoute] string title, [FromBody] EducationForEditDto body)
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
            return Ok("Educación eliminada correctamente.");
        }
    }
}
