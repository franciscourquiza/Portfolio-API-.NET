using Application.Dtos.ProyectDtos;
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
    public class ProyectController : ControllerBase
    {
        private readonly IProyectService _service; 
        private readonly IValidator<ProyectForAddDto> _validator;
        private readonly IValidator<ProyectForEditDto> _validatorForEdit;
        public ProyectController(IProyectService service, IValidator<ProyectForAddDto> validator, IValidator<ProyectForEditDto> validatorForEdit)
        {
            _service = service;
            _validator = validator;
            _validatorForEdit = validatorForEdit;
        }
        [HttpGet("GetByTitle/{title}", Name = "GetProyectByTitle")]
        public async Task<IActionResult> GetProyectByTitle([FromRoute] string title)
        {
            Proyect? proyect = await _service.GetByTitle(title);
            if (proyect == null)
            {
                return NotFound("Proyecto no encontrado");
            }
            return Ok(proyect);
        }
        [HttpGet("GetById{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Proyect? proyect = await _service.Get(id);
            if (proyect == null)
            {
                return NotFound("Proyecto no encontrado");
            }
            return Ok(proyect);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.Get());
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Add([FromBody] ProyectForAddDto body)
        {
            var validationResult = await _validator.ValidateAsync(body);
            if (!validationResult.IsValid) 
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            string? userEmail = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
            Proyect createdProyect = await _service.Add(body, userEmail);
            return CreatedAtRoute(nameof(GetProyectByTitle), new { title = createdProyect.Title }, createdProyect);
        }

        [HttpPut("UpdateByTitle/{title}")]
        public async Task<IActionResult> UpdateProyect([FromRoute] string title, [FromBody] ProyectForEditDto body)
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
            return Ok("Proyecto eliminado correctamente.");
        }
    }
}
