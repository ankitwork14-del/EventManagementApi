namespace EventManagementApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenService _jwt;
        public AuthService(IJwtTokenService jwt) => _jwt = jwt;

        public Task<string?> LoginAsync(string username, string password)
        {
            if (username == "admin" && password == "password123")//hard coded for demo
            {
                var token = _jwt.GenerateToken(username, "Admin");//if the login is  successful it will generate tokens 
                return Task.FromResult<string?>(token);
            }

            return Task.FromResult<string?>(null);
        }
    }
}
