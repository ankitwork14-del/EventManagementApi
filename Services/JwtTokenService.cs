using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManagementApi.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;
        public JwtTokenService(IConfiguration config) => _config = config;
       
        //method to generate tokens
        public string GenerateToken(string username, string role)
        {
            var jwtSettings = _config.GetSection("Jwt");//Read jwt settings from appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));//turn shared key into bytes
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//uses Hmacsha algorithm for sign in

            var claims = new[]//adding claims
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

                var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"], //this help the server to verify token was issued by trusted application.
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
