using Application.Dtos.UserDtos;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace CleanArchitectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetByName/{name}")]
        public IActionResult GetByName([FromRoute]string name)
        {
            UserWithoutPasswordDto? user = _userService.GetUserWithoutPasswordByName(name);
            if (user == null)
            {
                return NotFound("Persona no encontrada");
            }
            return Ok(user);
        }

        [HttpGet("GetByEmail/{email}", Name = nameof(GetByEmail))]
        public IActionResult GetByEmail([FromRoute] string email)
        {
            UserWithoutPasswordDto user = _userService.GetUserWithoutPassword(email);
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }
            return Ok(user);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public IActionResult GetAll() 
        {
            string userRole = User.Claims.FirstOrDefault(c => c.Type.Contains("role")).Value;
            if (userRole == "Admin" || userRole == "SuperAdmin") 
            {
                return Ok(_userService.Get());
            }
            return Forbid();
        }

        [HttpPost("CreateAccount")] 
        public IActionResult AddUser([FromBody] UserForAddRequest body) 
        {
            if (_userService.GetEmailForCreation(body.Email) != null)
            {
                return Conflict("El email ya está en uso.");
            }
            if (body == null)
            {
                return BadRequest();
            }
            _userService.Add(body); 
            return CreatedAtRoute(nameof(GetByEmail), new { email = body.Email }, body);
        }

        [HttpPut("EditAccount")] 
        [Authorize]
        public IActionResult UpdateUser([FromBody] UserForEditDto body)
        {

            if (body == null) { return BadRequest(); }
            string userEmail = User.Claims.SingleOrDefault(c => c.Type.Contains("nameidentifier")).Value; 
            _userService.Update(body, userEmail);
            return Ok(body);
        }

        [HttpDelete("DeleteByEmail/{email}")] 
        [Authorize]
        public IActionResult DeleteUserByEmail([FromRoute] string email) 
        {
            string role = User.Claims.SingleOrDefault(c => c.Type.Contains("role")).Value; 
            if (role == "SuperAdmin")
            {
               _userService.Delete(email);
                return NoContent();
            }
            return Forbid();
        }
    }
}
