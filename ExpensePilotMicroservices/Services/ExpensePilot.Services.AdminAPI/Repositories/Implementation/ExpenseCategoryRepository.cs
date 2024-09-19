using ExpensePilot.Services.AdminAPI.Models.Domain;
using ExpensePilot.Services.AdminAPI.Repositories.Interface;
using ExpensePilot.Services.AuthenticationAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpensePilot.Services.AdminAPI.Repositories.Implementation
{
    public class ExpenseCategoryRepository:IExpenseCategoryRepository
    {
        private readonly AdminDbContext dbContext;

        public ExpenseCategoryRepository(AdminDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ExpenseCategory> CreateAsync(ExpenseCategory expenseCategory)
        {
            await dbContext.ExpenseCategory.AddAsync(expenseCategory);
            await dbContext.SaveChangesAsync();
            return expenseCategory;
        }

        public async Task<ExpenseCategory?> DeleteAsync(int id)
        {
            var existingCategory = await dbContext.ExpenseCategory.FirstOrDefaultAsync(ec => ec.CategoryID == id);
            if (existingCategory is null)
            {
                return null;
            }
            dbContext.ExpenseCategory.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAllAsync()
        {
            return await dbContext.ExpenseCategory.ToListAsync();
        }

        public async Task<ExpenseCategory?> GetByIdAsync(int id)
        {
            return await dbContext.ExpenseCategory.FirstOrDefaultAsync(ec => ec.CategoryID == id);
        }

        public async Task<ExpenseCategory?> UpdateAsync(ExpenseCategory expenseCategory)
        {
            var existingCategory = await dbContext.ExpenseCategory.FirstOrDefaultAsync(ec => ec.CategoryID == expenseCategory.CategoryID);
            if (existingCategory is null)
            {
                return null;
            }
            dbContext.Entry(existingCategory).CurrentValues.SetValues(expenseCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}
