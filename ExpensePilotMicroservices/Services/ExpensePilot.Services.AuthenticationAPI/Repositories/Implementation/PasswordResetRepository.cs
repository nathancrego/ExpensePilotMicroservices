using ExpensePilot.Services.AuthenticationAPI.Models.Domain;
using ExpensePilot.Services.AuthenticationAPI.Repositories.Interface;
using Microsoft.AspNetCore.Identity;

namespace ExpensePilot.Services.AuthenticationAPI.Repositories.Implementation
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private readonly UserManager<User> userManager;

        public PasswordResetRepository(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<User> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            if(string.IsNullOrEmpty(user.SecurityStamp))
            {
                user.SecurityStamp = Guid.NewGuid().ToString();
                await userManager.UpdateAsync(user);
            }
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            return await userManager.ResetPasswordAsync(user, token, newPassword);
        }
    }
}
