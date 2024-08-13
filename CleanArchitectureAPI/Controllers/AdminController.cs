using Application.Dtos.AdminDtos;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace CleanArchitectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _service;
        public AdminController(AdminService service)
        {
            _service = service;
        }

        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            string userRole = User.Claims.FirstOrDefault(c => c.Type.Contains("role")).Value;
            Admin? user = _service.Get(name);
            if (userRole == "SuperAdmin")
            {
                return Ok(user);
            }
            if (user == null)
            {
                return NotFound("Admin no encontrado");
            }
            return Forbid();
        }

        [HttpGet("GetByEmail/{email}", Name = nameof(GetAdminByEmail))]
        public IActionResult GetAdminByEmail([FromRoute] string email)
        {
            string userRole = User.Claims.FirstOrDefault(c => c.Type.Contains("role")).Value;
            Admin? user = _service.GetByEmail(email);
            if (userRole == "SuperAdmin")
            {
                return Ok(user);
            }
            if (user == null)
            {
                return NotFound("Admin no encontrado");
            }
            return Forbid();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            string userRole = User.Claims.FirstOrDefault(c => c.Type.Contains("role")).Value;
            if (userRole == "SuperAdmin")
            {
                return Ok(_service.Get());
            }
            return Forbid();
        }

        [HttpPost("Create")]
        public IActionResult AddAdmin([FromBody] AdminForAddDto body)
        {
            if (_service.GetByEmail(body.Email) != null)
            {
                return Conflict("El email ya está en uso.");
            }
            if (body == null)
            {
                return BadRequest();
            }
            _service.AddAdmin(body);
            return CreatedAtRoute(nameof(GetAdminByEmail), new { email = body.Email }, body);
        }

        [HttpPut("UpdateByEmail/{email}")]
        public IActionResult UpdateAdmin([FromBody] AdminForEditDto body, [FromRoute] string email)
        {
            string userRole = User.Claims.SingleOrDefault(c => c.Type.Contains("role")).Value;
            if (body == null) { return BadRequest(); }
            if (userRole == "SuperAdmin")
            {
                _service.Update(body, email);
                return Ok();
            }
            return Forbid();
        }

        [HttpDelete("DeleteByEmail/{email}")]
        public IActionResult DeleteUserByEmail([FromRoute] string email)
        {
            string role = User.Claims.SingleOrDefault(c => c.Type.Contains("role")).Value;
            if (role == "SuperAdmin")
            {
                _service.Delete(email);
                return NoContent();
            }
            return Forbid();
        }
    }
}
