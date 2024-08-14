using ExpensePilot.Services.PoliciesAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpensePilot.Services.PoliciesAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
            public DbSet<Policy> Policies { get; set; }
    }
}
