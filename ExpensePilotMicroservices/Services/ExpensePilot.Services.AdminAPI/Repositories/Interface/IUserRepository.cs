using ExpensePilot.Services.AuthenticationAPI.Models.Domain;

namespace ExpensePilot.Services.AuthenticationAPI.Repositories.Implementation
{
    public interface IUserRepository
    {
        public Task<User> CreateAsync(User user, string roleName);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User?> GetByIdAsync (Guid id);
        public Task<User?> UpdateAsync(User user, string newroleName);
        public Task<User?> DeleteAsync (Guid id);
        public Task<IList<string>> GetRoleAsync(User user);
    }
}
