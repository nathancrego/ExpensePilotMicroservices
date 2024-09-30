
using ExpensePilot.Services.ExpenseAPI.Models.Domain;
using ExpensePilot.Services.ExpenseAPI.Models.DTO;
using ExpensePilot.Services.ExpenseAPI.Repositories.Implementation;
using ExpensePilot.Services.ExpenseAPI.Repositories.Interface;
using ExpensePilot.Services.ExpenseAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ExpensePilot.Services.ExpenseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseRepository expenseRepository;
        private readonly IExpenseCategoryService expenseCategoryService;

        public ExpensesController(IExpenseRepository expenseRepository, IExpenseCategoryService expenseCategoryService)
        {
            this.expenseRepository = expenseRepository;
            this.expenseCategoryService = expenseCategoryService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> CreateExpense([FromForm] InvoiceReceiptUploadDto receiptUploadDto, [FromForm] CreateExpenseDto createExpense)
        {
            if (receiptUploadDto.File == null || createExpense == null)
            {
                return BadRequest("Please upload the Invoice receipt and fill the required details in the Expense Form");
            }
            //fetch all expense categories from admin api
            var categories = await expenseCategoryService.GetAllExpenseCategoriesAsync();

            //check if provided expensecategoryid is valid
            var selectedCategory = categories.FirstOrDefault(c=>c.CategoryID == createExpense.ExpenseCategoryID);
            if (selectedCategory == null)
            {
                return BadRequest($"Invalid Expense Category with ID: {createExpense.ExpenseCategoryID}");
            }

            var expense = new Expense
            {
                ExpenseName = createExpense.ExpenseName,
                ExpenseDescription = createExpense.ExpenseDescription,
                ExpenseCategoryID = createExpense.ExpenseCategoryID,
                InvoiceNumber = createExpense.InvoiceNumber,
                InvoiceVendorName = createExpense.InvoiceVendorName,
                InvoiceDate = createExpense.InvoiceDate,
                TotalAmount = createExpense.TotalAmount,
                //UserID = createExpense.UserID,
                //ExpenseStatusID = createExpense.ExpenseStatusID,
                CreatedDate = DateTime.UtcNow,
           
            };

            //Creates the expense and uploads the receipt
            expense = await expenseRepository.CreateAsync(expense, receiptUploadDto);
            var response = new ExpenseDto
            {
                ExpenseID = expense.ExpenseID,
                ExpenseName = expense.ExpenseName,
                ExpenseDescription = expense.ExpenseDescription,
                ExpenseCategoryID = expense.ExpenseCategoryID,
                InvoiceNumber = expense.InvoiceNumber,
                InvoiceVendorName = expense.InvoiceVendorName,
                InvoiceDate = expense.InvoiceDate,
                TotalAmount = expense.TotalAmount,
                InvoiceReceiptID = expense.InvoiceReceiptID,
                UserID = expense.UserID,
                ExpenseStatusID = expense.ExpenseStatusID,
                CreatedDate = expense.CreatedDate
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var expenses = await expenseRepository.GetAllAsync();

            var response = new List<ExpenseDto>();
            foreach (var expense in expenses)
            {
                response.Add(new ExpenseDto
                {
                    ExpenseID = expense.ExpenseID,
                    ExpenseName = expense.ExpenseName,
                    ExpenseDescription = expense.ExpenseDescription,
                    ExpenseCategoryID = expense.ExpenseCategoryID,
                    InvoiceNumber = expense.InvoiceNumber,
                    InvoiceVendorName = expense.InvoiceVendorName,
                    InvoiceDate = expense.InvoiceDate,
                    TotalAmount = expense.TotalAmount,
                    InvoiceReceiptID = expense.InvoiceReceiptID,
                    UserID = expense.UserID,
                    ExpenseStatusID = expense.ExpenseStatusID,
                    CreatedDate = expense.CreatedDate,
                    SubmittedDate = expense.SubmittedDate,
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetExpenseID([FromRoute] int id)
        {
            var existingExpense = await expenseRepository.GetByIDAsync(id);
            if (existingExpense == null)
            {
                return NotFound();
            }
            var response = new ExpenseDto
            {
                ExpenseID = existingExpense.ExpenseID,
                ExpenseName = existingExpense.ExpenseName,
                ExpenseDescription = existingExpense.ExpenseDescription,
                ExpenseCategoryID = existingExpense.ExpenseCategoryID,
                InvoiceNumber = existingExpense.InvoiceNumber,
                InvoiceVendorName = existingExpense.InvoiceVendorName,
                InvoiceDate = existingExpense.InvoiceDate,
                TotalAmount = existingExpense.TotalAmount,
                InvoiceReceiptID = existingExpense.InvoiceReceiptID,
                UserID = existingExpense.UserID,
                ExpenseStatusID = existingExpense.ExpenseStatusID,
                CreatedDate = existingExpense.CreatedDate,
                SubmittedDate = existingExpense.SubmittedDate
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateExpense([FromRoute] int id, [FromForm] InvoiceReceiptUploadDto receiptUploadDto, [FromForm] EditExpenseDto editExpense)
        {
            // Validate the input DTOs
            if (receiptUploadDto == null || editExpense == null)
            {
                return BadRequest("Invalid request payload.");
            }

            //fetch all expense categories from admin api
            var categories = await expenseCategoryService.GetAllExpenseCategoriesAsync();

            //check if provided expensecategoryid is valid
            var selectedCategory = categories.FirstOrDefault(c => c.CategoryID == editExpense.ExpenseCategoryID);
            if (selectedCategory == null)
            {
                return BadRequest($"Invalid Expense Category with ID: {editExpense.ExpenseCategoryID}");
            }

            // Create an Expenses object with the new data
            var updateExpense = new Expense
            {
                ExpenseID = id, // Ensure the ID is correctly set
                ExpenseName = editExpense.ExpenseName,
                ExpenseDescription = editExpense.ExpenseDescription,
                ExpenseCategoryID = editExpense.ExpenseCategoryID,
                InvoiceNumber = editExpense.InvoiceNumber,
                InvoiceVendorName = editExpense.InvoiceVendorName,
                InvoiceDate = editExpense.InvoiceDate,
                TotalAmount = editExpense.TotalAmount,
                //UserID = editExpense.UserID,
                //ExpenseStatusID = editExpense.ExpenseStatusID
            };

            // Call the update method
            var updatedExpense = await expenseRepository.UpdateAsync(updateExpense, receiptUploadDto);

            // Check if the update was successful
            if (updatedExpense == null)
            {
                return NotFound("Expense or InvoiceReceipt not found.");
            }

            // Create a response DTO
            var response = new ExpenseDto
            {
                ExpenseID = updatedExpense.ExpenseID,
                ExpenseName = updatedExpense.ExpenseName,
                ExpenseDescription = updatedExpense.ExpenseDescription,
                ExpenseCategoryID = updatedExpense.ExpenseCategoryID,
                InvoiceNumber = updatedExpense.InvoiceNumber,
                InvoiceVendorName = updatedExpense.InvoiceVendorName,
                InvoiceDate = updatedExpense.InvoiceDate,
                TotalAmount = updatedExpense.TotalAmount,
                InvoiceReceiptID = updatedExpense.InvoiceReceiptID,
                UserID = updatedExpense.UserID,
                ExpenseStatusID = updatedExpense.ExpenseStatusID,
                CreatedDate = updatedExpense.CreatedDate,
                SubmittedDate = updatedExpense.SubmittedDate
            };

            // Return the response
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteExpense([FromRoute] int id)
        {
            var deleteExpense = await expenseRepository.DeleteAsync(id);
            if (deleteExpense == null)
            {
                return NotFound();
            }
            var response = new ExpenseDto
            {
                ExpenseID = deleteExpense.ExpenseID,
                ExpenseName = deleteExpense.ExpenseName,
                ExpenseDescription = deleteExpense.ExpenseDescription,
                ExpenseCategoryID = deleteExpense.ExpenseCategoryID,
                InvoiceNumber = deleteExpense.InvoiceNumber,
                InvoiceVendorName = deleteExpense.InvoiceVendorName,
                TotalAmount = deleteExpense.TotalAmount,
                UserID = deleteExpense.UserID,
                ExpenseStatusID = deleteExpense.ExpenseStatusID,
                CreatedDate = deleteExpense.CreatedDate,
                SubmittedDate = deleteExpense.SubmittedDate

            };
            return Ok(response);
        }

    }
}
