using ExpensePilot.Services.AdminAPI.Models.Domain;
using ExpensePilot.Services.AuthenticationAPI.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpensePilot.Services.AuthenticationAPI.Data
{
    //Always remember the reference to model classes should be in the same order as given in the intellisense.
    public class AdminDbContext :IdentityDbContext<User,Role,Guid,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    { 
        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options) { }

        public DbSet<User> Users {  get; set; }
        public DbSet<ExpenseCategory> ExpenseCategory { get; set; }
        public DbSet<ExpenseStatus> ExpenseStatus { get; set; }
        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Self-referencing relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Manager)
                .WithMany(u => u.Subordinates)
                .HasForeignKey(u => u.ManagerId)
                .OnDelete(DeleteBehavior.Restrict); //Prevent cascade delete

            //User - Department relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId);
        }
    }
}
