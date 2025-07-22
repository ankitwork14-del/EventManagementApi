namespace EventManagementApi.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string username, string role);
    }
}
