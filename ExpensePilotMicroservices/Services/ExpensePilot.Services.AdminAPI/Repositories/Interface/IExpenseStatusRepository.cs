using ExpensePilot.Services.AdminAPI.Models.Domain;

namespace ExpensePilot.Services.AdminAPI.Repositories.Interface
{
    public interface IExpenseStatusRepository
    {
        Task<ExpenseStatus> CreateAsync(ExpenseStatus expenseStatus);
        Task<IEnumerable<ExpenseStatus>> GetAllAsync();
        Task<ExpenseStatus?> GetByIdAsync(int id);
        Task<ExpenseStatus?> UpdateAsync(ExpenseStatus expenseStatus);
        Task<ExpenseStatus?> DeleteAsync(int id);
    }
}
