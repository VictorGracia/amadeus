using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Método para el inicio de sesión
        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] AuthenticationModel authModel)
        {
            // Valida si el modelo de entrada es nulo
            if (authModel == null || string.IsNullOrWhiteSpace(authModel.Username) || string.IsNullOrWhiteSpace(authModel.Password))
            {
                return BadRequest("Username y Password son requeridos.");
            }

            // valida las credenciales del usuario
            if (IsValidUser(authModel.Username, authModel.Password))
            {
                var key = _configuration["Jwt:Key"];
                if (string.IsNullOrEmpty(key))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "La clave JWT no está configurada.");
                }

                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, authModel.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddMinutes(30),
                    claims: claims,
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();
        }

        private bool IsValidUser(string username, string password)
        {
            return username == "test" && password == "password";
        }
    }
}
