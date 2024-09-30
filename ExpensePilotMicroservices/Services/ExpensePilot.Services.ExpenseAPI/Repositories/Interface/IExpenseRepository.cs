using ExpensePilot.Services.ExpenseAPI.Models.Domain;
using ExpensePilot.Services.ExpenseAPI.Models.DTO;

namespace ExpensePilot.Services.ExpenseAPI.Repositories.Interface
{
    public interface IExpenseRepository
    {
        Task<Expense> CreateAsync(Expense expenses, InvoiceReceiptUploadDto invoiceReceiptUpload);
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<Expense?> GetByIDAsync(int id);
        Task<Expense?> UpdateAsync(Expense updatedExpenses, InvoiceReceiptUploadDto invoiceReceiptUpload);
        Task<Expense?> DeleteAsync(int id);
    }
}
