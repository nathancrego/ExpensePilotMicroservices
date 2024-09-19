using ExpensePilot.Services.AdminAPI.Models.Domain;
using ExpensePilot.Services.AdminAPI.Models.DTO;
using ExpensePilot.Services.AdminAPI.Repositories.Implementation;
using ExpensePilot.Services.AdminAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePilot.Services.AdminAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ExpenseCategoriesController : ControllerBase
    {
        private readonly IExpenseCategoryRepository expenseCategoryRepository;
        public ExpenseCategoriesController(IExpenseCategoryRepository expenseCategoryRepository)
        {
            this.expenseCategoryRepository = expenseCategoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpenseCategory(AddExpenseCategoryDto addExpenseCategory)
        {
            var addCategory = new ExpenseCategory
            {
                CategoryName = addExpenseCategory.CategoryName,
                LastUpdated = addExpenseCategory.LastUpdated
            };
            await expenseCategoryRepository.CreateAsync(addCategory);
            var response = new ExpenseCategoryDto
            {
                CategoryID = addCategory.CategoryID,
                CategoryName = addCategory.CategoryName,
                LastUpdated = addCategory.LastUpdated
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenseCategory()
        {
            var categories = await expenseCategoryRepository.GetAllAsync();
            var response = new List<ExpenseCategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new ExpenseCategoryDto
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    LastUpdated = category.LastUpdated
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetExpenseCategoryByID([FromRoute] int id)
        {
            var existingCategory = await expenseCategoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            var response = new ExpenseCategoryDto
            {
                CategoryID = existingCategory.CategoryID,
                CategoryName = existingCategory.CategoryName,
                LastUpdated = existingCategory.LastUpdated
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditExpenseCategory([FromRoute] int id, EditExpenseCategoryDto editExpenseCategory)
        {
            var editCategory = new ExpenseCategory
            {
                CategoryID = id,
                CategoryName = editExpenseCategory.CategoryName,
                LastUpdated = editExpenseCategory.LastUpdated
            };
            editCategory = await expenseCategoryRepository.UpdateAsync(editCategory);
            if (editCategory == null)
            {
                return NotFound();
            }
            var response = new ExpenseCategoryDto
            {
                CategoryID = editCategory.CategoryID,
                CategoryName = editCategory.CategoryName,
                LastUpdated = editCategory.LastUpdated
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteExpenseCategory([FromRoute] int id)
        {
            var existingCategory = await expenseCategoryRepository.DeleteAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            var response = new ExpenseCategoryDto
            {
                CategoryID = existingCategory.CategoryID,
                CategoryName = existingCategory.CategoryName,
                LastUpdated = existingCategory.LastUpdated
            };
            return Ok(response);
        }
    }
}
