using ExpensePilot.Services.AuthenticationAPI.Models.Domain;

namespace ExpensePilot.Services.AdminAPI.Repositories.Interface
{
    public interface IUserRoleRepository
    {
        public Task<UserRole> CreateAsync(UserRole userRole);
        public Task<UserRole?> UpdateAsync(UserRole userRole);
        public Task<UserRole?> DeleteAsync(Guid id);
        public Task<UserRole?> GetByIdAsync(Guid id);
        public Task<IEnumerable<UserRole?>> GetAllAsync();
    }
}
