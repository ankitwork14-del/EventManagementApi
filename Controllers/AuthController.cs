using EventManagementApi.Models;
using EventManagementApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            var token = await _auth.LoginAsync(user.Username, user.Password);
            if (token is null)
                return Unauthorized("Invalid credentials");

            return Ok(new { Token = token });
        }
    }
}
