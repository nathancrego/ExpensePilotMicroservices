using ExpensePilot.Services.AuthenticationAPI.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace ExpensePilot.Services.AuthenticationAPI.Repositories.Interface
{
    public interface IPasswordResetRepository
    {
        Task<User> FindByEmailAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
    }
}
