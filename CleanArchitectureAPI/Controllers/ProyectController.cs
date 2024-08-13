using Application.Dtos.ProyectDtos;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProyectController : ControllerBase
    {
        private readonly ProyectService _service;
        public ProyectController(ProyectService service)
        {
            _service = service;
        }
        [HttpGet("GetByTitle/{title}", Name = "GetProyectByTitle")]
        public IActionResult GetProyectByTitle([FromRoute] string title)
        {
            Proyect? proyect = _service.GetByTitle(title);
            if (proyect == null)
            {
                return NotFound("Proyecto no encontrado");
            }
            return Ok(proyect);
        }
        [HttpGet("GetById{id}")]
        public IActionResult Get(int id)
        {
            var proyect = _service.Get(id);
            if (proyect == null)
            {
                return NotFound("Proyecto no encontrado");
            }
            return Ok(proyect);
        }
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(_service.Get());
        }
        [HttpPost("Create")]
        public IActionResult Add([FromBody] ProyectForAddDto body)
        {
            string? userEmail = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
            Proyect createdProyect = _service.Add(body, userEmail);
            return CreatedAtRoute(nameof(GetProyectByTitle), new { title = createdProyect.Title }, createdProyect);
        }

        [HttpPut("UpdateByTitle/{title}")]
        public IActionResult UpdateProyect([FromRoute] string title, [FromBody] ProyectForEditDto body)
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
            return Ok("Proyecto eliminado correctamente.");
        }
    }
}
