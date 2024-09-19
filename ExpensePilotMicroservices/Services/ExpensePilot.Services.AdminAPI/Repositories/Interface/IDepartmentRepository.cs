using ExpensePilot.Services.AdminAPI.Models.Domain;

namespace ExpensePilot.Services.AdminAPI.Repositories.Interface
{
    public interface IDepartmentRepository
    {
        Task<Department> CreateAsync(Department department);
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int id);
        Task<Department> UpdateAsync(Department department);
        Task<Department> DeleteAsync(int id);
    }
}
