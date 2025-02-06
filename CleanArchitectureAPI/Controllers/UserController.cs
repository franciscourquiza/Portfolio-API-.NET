using Application.Dtos.UserDtos;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserForAddRequest> _validatorForAdd;
        private readonly IValidator<UserForEditDto> _validatorForEdit;

        public UserController(IUserService userService, IValidator<UserForAddRequest> validatorForAdd, IValidator<UserForEditDto> validatorForEdit)
        {
            _userService = userService;
            _validatorForAdd = validatorForAdd;
            _validatorForEdit = validatorForEdit;
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetUserByName([FromRoute]string name)
        {
            UserWithoutPasswordDto? user = await _userService.GetUserWithoutPasswordByName(name);
            if (user == null)
            {
                return NotFound("Persona no encontrada.");
            }
            return Ok(user);
        }

        [HttpGet("GetByEmail/{email}", Name = nameof(GetUserByEmail))]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
        {
            UserWithoutPasswordDto? user = await _userService.GetUserWithoutPasswordByEmail(email);
            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }
            return Ok(user);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll() 
        {
            string userRole = User.Claims.FirstOrDefault(c => c.Type.Contains("role")).Value;
            if (userRole == "Admin" || userRole == "SuperAdmin")
            {
                return Ok(await _userService.GetAllUsers());
            }
            return Forbid();
        }

        [HttpPost("CreateAccount")] 
        public async Task<IActionResult> AddUser([FromBody] UserForAddRequest body) 
        {
            var validationResult = await _validatorForAdd.ValidateAsync(body);
            if (!validationResult.IsValid) 
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            if (await _userService.GetUserWithoutPasswordByEmail(body.Email) != null)
            {
                return Conflict("El email ya está en uso.");
            }
            if (body == null)
            {
                return BadRequest();
            }
            await _userService.CreateUser(body);
            return CreatedAtRoute(nameof(GetUserByEmail), new { email = body.Email }, body);
        }

        [HttpPut("EditAccount")] 
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserForEditDto body)
        {
            var validationResult = await _validatorForEdit.ValidateAsync(body);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            if (body == null) { return BadRequest(); }
            string userEmail = User.Claims.SingleOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            await _userService.UpdateUser(body, userEmail);
            return Ok(body);
        }

        [HttpDelete("DeleteByEmail/{email}")] 
        [Authorize]
        public async Task<IActionResult> DeleteUserByEmail([FromRoute] string email) 
        {
            string role = User.Claims.SingleOrDefault(c => c.Type.Contains("role")).Value;
            if (role == "SuperAdmin") 
            {
                await _userService.DeleteUserByEmail(email);
                return Ok("Se eliminó exitosamente el usuario.");
            }
            return Forbid();
        }
    }
}
