using ExpensePilot.Services.AdminAPI.Repositories.Interface;
using ExpensePilot.Services.AuthenticationAPI.Data;
using ExpensePilot.Services.AuthenticationAPI.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpensePilot.Services.AdminAPI.Repositories.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AdminDbContext dbContext;
        private readonly RoleManager<Role> roleManager;

        public RoleRepository(AdminDbContext dbContext, RoleManager<Role> roleManager)
        {
            this.dbContext = dbContext;
            this.roleManager = roleManager;
        }
        public async Task<Role> CreateAsync(Role role)
        {
            await dbContext.Roles.AddAsync(role);
            await dbContext.SaveChangesAsync();
            return role;
        }

        public async Task<Role?> DeleteAsync(Guid id)
        {
            var existingRole = await dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRole is null)
            {
                return null;
            }
            dbContext.Roles.Remove(existingRole);
            await dbContext.SaveChangesAsync();
            return existingRole;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await dbContext.Roles.ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(Guid id)
        {
            return await dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role?> UpdateAsync(Role role)
        {
            var existingRole = await dbContext.Roles.FirstOrDefaultAsync(r => r.Id == role.Id);
            if(existingRole is null)
            {
                return null;
            }
            dbContext.Entry(existingRole).CurrentValues.SetValues(role);
            await dbContext.SaveChangesAsync();
            return existingRole;
        }
    }
}
