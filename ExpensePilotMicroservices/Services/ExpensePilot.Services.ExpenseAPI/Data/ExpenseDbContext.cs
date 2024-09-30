using ExpensePilot.Services.ExpenseAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpensePilot.Services.ExpenseAPI.Data
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) { }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<InvoiceReceipt> InvoiceReceipts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure Expense and InvoiceReceipt relationship
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.InvoiceReceipt)
                .WithOne(i => i.Expense)
                .HasForeignKey<Expense>(e => e.InvoiceReceiptID)
                .IsRequired();
        }
    }
}
