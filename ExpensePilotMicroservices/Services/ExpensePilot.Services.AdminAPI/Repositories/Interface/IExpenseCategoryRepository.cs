using ExpensePilot.Services.AdminAPI.Models.Domain;

namespace ExpensePilot.Services.AdminAPI.Repositories.Interface
{
    public interface IExpenseCategoryRepository
    {
        Task<ExpenseCategory> CreateAsync(ExpenseCategory expenseCategory);
        Task<IEnumerable<ExpenseCategory>> GetAllAsync();
        Task<ExpenseCategory?> GetByIdAsync(int id);
        Task<ExpenseCategory?> UpdateAsync(ExpenseCategory expenseCategory);
        Task<ExpenseCategory?> DeleteAsync(int id);
    }
}
