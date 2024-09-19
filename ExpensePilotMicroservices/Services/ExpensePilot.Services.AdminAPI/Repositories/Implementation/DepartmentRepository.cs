using ExpensePilot.Services.AdminAPI.Models.Domain;
using ExpensePilot.Services.AdminAPI.Repositories.Interface;
using ExpensePilot.Services.AuthenticationAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpensePilot.Services.AdminAPI.Repositories.Implementation
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AdminDbContext dbContext;

        public DepartmentRepository(AdminDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Department> CreateAsync(Department department)
        {
            await dbContext.Department.AddAsync(department);
            await dbContext.SaveChangesAsync();
            return department;
        }

        public async Task<Department> DeleteAsync(int id)
        {
            var existingDepartment = await dbContext.Department.FirstOrDefaultAsync(d => d.DptID == id);
            if (existingDepartment is null)
            {
                return null;
            }
            dbContext.Department.Remove(existingDepartment);
            await dbContext.SaveChangesAsync();
            return existingDepartment;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await dbContext.Department.ToListAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await dbContext.Department.FirstOrDefaultAsync(d => d.DptID == id);
        }

        public async Task<Department> UpdateAsync(Department department)
        {
            var existingDepartment = await dbContext.Department.FirstOrDefaultAsync(d => d.DptID == department.DptID);
            if (existingDepartment is null)
            {
                return null;
            }
            dbContext.Entry(existingDepartment).CurrentValues.SetValues(department);
            await dbContext.SaveChangesAsync();
            return department;
        }
    }
}
