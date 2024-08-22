using ExpensePilot.Services.AuthenticationAPI.Data;
using ExpensePilot.Services.AuthenticationAPI.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpensePilot.Services.AuthenticationAPI.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AdminDbContext dbContext;
        private readonly UserManager<User> userManager;

        public UserRepository(AdminDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<User> CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            await dbContext.Entry(user).Reference(u=>u.Manager).LoadAsync();
            return user;
        }

        public async Task<User?> DeleteAsync(Guid id)
        {
            var existingUser = await dbContext.Users.Include(u => u.Manager).FirstOrDefaultAsync(u => u.Id == id);
            if(existingUser is null)
            {
                return null;
            }
            dbContext.Users.Remove(existingUser);
            await dbContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbContext.Users.Include(u=>u.Manager).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await dbContext.Users.Include(u=>u.Manager).FirstOrDefaultAsync(u=>u.Id == id);
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existingUser = await dbContext.Users.Include(u=>u.Manager).FirstOrDefaultAsync(u=>u.Id==user.Id);
            if(existingUser is null)
            {
                return null;
            }
            existingUser.Fname = user.Fname;
            existingUser.Lname = user.Lname;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.ManagerId = user.ManagerId;
            await dbContext.SaveChangesAsync();
            //Reload navigation properties
            await dbContext.Entry(existingUser).Reference(u => u.Manager).LoadAsync();
            return user;
        }
    }
}
