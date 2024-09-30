using ExpensePilot.Services.ExpenseAPI.Models.DTO;

namespace ExpensePilot.Services.ExpenseAPI.Services.Interface
{
    public interface IExpenseCategoryService
    {
        public Task<List<ExpenseCategoryDto>> GetAllExpenseCategoriesAsync();
    }
}
