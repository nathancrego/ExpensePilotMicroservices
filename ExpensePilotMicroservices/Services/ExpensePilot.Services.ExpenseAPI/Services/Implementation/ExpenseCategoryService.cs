using ExpensePilot.Services.ExpenseAPI.Models.DTO;
using ExpensePilot.Services.ExpenseAPI.Services.Interface;
using Newtonsoft.Json;

namespace ExpensePilot.Services.ExpenseAPI.Services.Implementation
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly HttpClient httpClient;

        public ExpenseCategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<List<ExpenseCategoryDto>> GetAllExpenseCategoriesAsync()
        {
            var response = await httpClient.GetAsync("https://localhost:7002/api/admin/ExpenseCategories"); //AdminAPI URL
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ExpenseCategoryDto>>(content);
            }
            return new List<ExpenseCategoryDto>();
        }
    }
}
