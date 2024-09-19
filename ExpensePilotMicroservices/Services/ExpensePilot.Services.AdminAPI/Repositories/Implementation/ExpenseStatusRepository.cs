using ExpensePilot.Services.AdminAPI.Models.Domain;
using ExpensePilot.Services.AdminAPI.Repositories.Interface;
using ExpensePilot.Services.AuthenticationAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpensePilot.Services.AdminAPI.Repositories.Implementation
{
    public class ExpenseStatusRepository:IExpenseStatusRepository
    {
        private readonly AdminDbContext dbContext;

        public ExpenseStatusRepository(AdminDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ExpenseStatus> CreateAsync(ExpenseStatus expenseStatus)
        {
            await dbContext.ExpenseStatus.AddAsync(expenseStatus);
            await dbContext.SaveChangesAsync();
            return expenseStatus;
        }

        public async Task<ExpenseStatus?> DeleteAsync(int id)
        {
            var existingStatus = await dbContext.ExpenseStatus.FirstOrDefaultAsync(es => es.StatusID == id);
            if (existingStatus is null)
            {
                return null;
            }
            dbContext.ExpenseStatus.Remove(existingStatus);
            await dbContext.SaveChangesAsync();
            return existingStatus;
        }

        public async Task<IEnumerable<ExpenseStatus>> GetAllAsync()
        {
            return await dbContext.ExpenseStatus.ToListAsync();
        }

        public async Task<ExpenseStatus?> GetByIdAsync(int id)
        {
            return await dbContext.ExpenseStatus.FirstOrDefaultAsync(es => es.StatusID == id);
        }

        public async Task<ExpenseStatus?> UpdateAsync(ExpenseStatus expenseStatus)
        {
            var existingStatus = await dbContext.ExpenseStatus.FirstOrDefaultAsync(es => es.StatusID == expenseStatus.StatusID);
            if (existingStatus is null)
            {
                return null;
            }
            dbContext.Entry(existingStatus).CurrentValues.SetValues(expenseStatus);
            await dbContext.SaveChangesAsync();
            return existingStatus;
        }
    }
}
