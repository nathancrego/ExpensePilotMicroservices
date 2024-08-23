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
        public async Task<User> CreateAsync(User user, string roleName)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            await userManager.AddToRoleAsync(user, roleName);
            await dbContext.Entry(user).Reference(u => u.Manager).LoadAsync();
            return user;

        }


        public async Task<User?> DeleteAsync(Guid id)
        {
            var existingUser = await dbContext.Users.Include(u => u.Manager).FirstOrDefaultAsync(u => u.Id == id);
            if(existingUser is null)
            {
                return null;
            }
            var userRoles = await userManager.GetRolesAsync(existingUser);
            if(userRoles.Count>0)
            {
                await userManager.RemoveFromRolesAsync(existingUser, userRoles);
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
            return await dbContext.Users.Include(u => u.Manager).FirstOrDefaultAsync(u => u.Id == id);

            //var userWithRoles = await dbContext.Users.Include(u=>u.Manager).Where(u=>u.Id == id)
            //    .Select(u=>new
            //    {
            //        User = u,
            //        Roles = (from userRole in dbContext.UserRoles
            //                 join role in dbContext.Roles on userRole.RoleId equals role.Id
            //                 where userRole.UserId == u.Id
            //                 select role.Name).ToList()
            //    }).FirstOrDefaultAsync();
            //if (userWithRoles != null)
            //{
            //    userWithRoles.User.RoleNames = userWithRoles.Roles;
            //    return userWithRoles.User;
            //}

            //return null;
        }

        public async Task<User?> UpdateAsync(User user, string newroleName)
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

            var currentRoles = await userManager.GetRolesAsync(existingUser);
            if(currentRoles.Count > 0)
            {
                await userManager.RemoveFromRolesAsync(existingUser, currentRoles);
            }
            //add the new role
            await userManager.AddToRoleAsync(existingUser, newroleName);

            await dbContext.SaveChangesAsync();
            //Reload navigation properties
            await dbContext.Entry(existingUser).Reference(u => u.Manager).LoadAsync();
            return user;
        }
    }
}
