using ExpensePilot.Services.AuthenticationAPI.Models.Domain;

namespace ExpensePilot.Services.AdminAPI.Repositories.Interface
{
    public interface IRoleRepository
    {
        public Task<Role> CreateAsync(Role role);
        public Task<IEnumerable<Role>> GetAllAsync();
        public Task<Role?> GetByIdAsync(Guid id);
        public Task<Role?> UpdateAsync(Role role);
        public Task<Role?> DeleteAsync(Guid id);
    }
}
