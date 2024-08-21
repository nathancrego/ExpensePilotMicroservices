using ExpensePilot.Services.AuthenticationAPI.Models.Domain;

namespace ExpensePilot.Services.AuthenticationAPI.Repositories.Implementation
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
