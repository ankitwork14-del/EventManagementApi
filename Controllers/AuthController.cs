using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventManagementApi.Models;

namespace EventManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin user)
        {
            if (user.Username == "admin" && user.Password == "password123") // Hardcoded for demo
            {
                var token = GenerateToken(user.Username);//if the login is  successful it will generate tokens 
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials");
        }
        //This method Generate  JWT tokens 
        private string GenerateToken(string username)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));// Converts the secret key (from settings) into bytes.
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); //Uses the key and HmacSha256 algorithm to sign the token.
            //Claims are pieces of information about the user or entity inside the token.


            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],//issuer – Who created the token.


                audience: jwtSettings["Audience"],//issuer – Who created the token.


                claims: claims,//claims – Data about the user.


                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
