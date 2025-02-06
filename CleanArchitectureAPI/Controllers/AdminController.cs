using Application.Dtos.AdminDtos;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        private readonly IValidator<AdminForAddDto> _validator;
        private readonly IValidator<AdminForEditDto> _validatorForEdit;
        public AdminController(IAdminService service, IValidator<AdminForAddDto> validator, IValidator<AdminForEditDto> validatorForEdit)
        {
            _service = service;
            _validator = validator;
            _validatorForEdit = validatorForEdit;
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetAdminByName([FromRoute] string name)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type.Contains("role")).Value;
            if (role == "SuperAdmin")
            {
                return Ok(await _service.GetAdminByName(name));
            }
            return Forbid();
        }

        [HttpGet("GetByEmail/{email}", Name = nameof(GetAdminByEmail))]
        public async Task<IActionResult> GetAdminByEmail([FromRoute] string email)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type.Contains("role")).Value;
            if (role == "SuperAdmin")
            {
                return Ok(await _service.GetAdminByEmail(email));
            }
            return Forbid();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string role = User.Claims.FirstOrDefault(c => c.Type.Contains("role")).Value;
            if (role == "SuperAdmin")
            {
                return Ok(await _service.GetAllAdmins());
            }
            return Forbid();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddAdmin([FromBody] AdminForAddDto body)
        {
            var validationResult = await _validator.ValidateAsync(body);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            string role = User.Claims.FirstOrDefault(c => c.Type.Contains("role")).Value;
            if (role == "SuperAdmin")
            {
                if (await _service.GetAdminForCreation(body.Email) != null)
                {
                    return Conflict("El email ya está en uso.");
                }
                await _service.AddAdmin(body);
                return CreatedAtRoute(nameof(GetAdminByEmail), new { email = body.Email }, body);
            }
            return Forbid();
        }

        [HttpPut("UpdateByEmail/{email}")]
        public async Task<IActionResult> UpdateAdmin([FromBody] AdminForEditDto body, [FromRoute] string email)
        {
            var validationResult = await _validatorForEdit.ValidateAsync(body);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            string role = User.Claims.FirstOrDefault(c => c.Type.Contains("role")).Value;
            if (role == "SuperAdmin")
            {
                await _service.UpdateAdmin(body, email);
                return Ok();
            }
            return Forbid();
        }

        [HttpDelete("DeleteByEmail/{email}")]
        public async Task<IActionResult> DeleteAdminByEmail([FromRoute] string email)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type.Contains("role")).Value;
            if (role == "SuperAdmin")
            {
                await _service.DeleteAdminByEmail(email);
                return Ok("Se eliminó exitosamente el admin.");
            }
            return Forbid();
        }
    }
}
