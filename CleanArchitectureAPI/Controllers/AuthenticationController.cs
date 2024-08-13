using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Entities;
using Application.Dtos.AuthDtos;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace CleanArchitectureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthenticationService _service;
        
        public AuthenticationController(IConfiguration configuration, AuthenticationService service)
        {
            _configuration = configuration;
            _service = service;
        }

        [HttpPost("Login")]
        public IActionResult Authenticate([FromBody] AuthenticationBodyRequest credentials)
        {
            Tuple<bool, User?> validationResponse = _service.ValidateUser(credentials.Email, credentials.Password);
            if (!validationResponse.Item1 && validationResponse.Item2 == null)
            {
                return NotFound("Email no existente");
            }
            else if (!validationResponse.Item1 && validationResponse.Item2 != null)
                return Unauthorized("Contraseña incorrecta");

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var credentialsForLogin = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", validationResponse.Item2.Email));
            claimsForToken.Add(new Claim("given_name", validationResponse.Item2.Name));
            claimsForToken.Add(new Claim("role", validationResponse.Item2.UserRole)); // cambiar mas adelante

            var jwtToken = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claimsForToken,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            credentialsForLogin);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtToken);

            return Ok(tokenToReturn);
        }

        [HttpPost("RequestEmailForResetPassword")]
        public async Task<ActionResult<ResetPasswordResponse<bool>>> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentException("Email cannot be null or whitespace.");
            }
            var response = new ResetPasswordResponse<bool>();
            response.Status = true;
            response.Msg = "Se envió el correo para reestablecer tu contraseña.";
            response.Value = await _service.RequestResetPassword(request);
            return Ok(response);
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<ResetPasswordResponse<bool>>> ResetPassword([FromQuery] string token)
        { 
            var response = new ResetPasswordResponse<bool>();
            // Si el token entra por la url como debe ser en POST, no hace falta hacer decode del Token
            string decodedToken = WebUtility.UrlDecode(token);
            response.Status = true;
            response.Msg = "Password reset successfully";
            response.Value = await _service.ResetPassword(token);
            return Ok(response);
        }

    }
}
