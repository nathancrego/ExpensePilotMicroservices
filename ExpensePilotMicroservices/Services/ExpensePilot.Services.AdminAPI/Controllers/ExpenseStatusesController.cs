using ExpensePilot.Services.AdminAPI.Models.Domain;
using ExpensePilot.Services.AdminAPI.Models.DTO;
using ExpensePilot.Services.AdminAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePilot.Services.AdminAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ExpenseStatusesController : ControllerBase
    {
        private readonly IExpenseStatusRepository expenseStatusRepository;

        public ExpenseStatusesController(IExpenseStatusRepository expenseStatusRepository)
        {
            this.expenseStatusRepository = expenseStatusRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpenseStatus(AddExpenseStatusDto addExpenseStatus)
        {
            var addStatus = new ExpenseStatus
            {
                StatusName = addExpenseStatus.StatusName,
                LastUpdated = addExpenseStatus.LastUpdated
            };
            await expenseStatusRepository.CreateAsync(addStatus);
            var response = new ExpenseStatusDto
            {
                StatusID = addStatus.StatusID,
                StatusName = addExpenseStatus.StatusName,
                LastUpdated = addStatus.LastUpdated
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenseStatus()
        {
            var statuses = await expenseStatusRepository.GetAllAsync();
            var response = new List<ExpenseStatusDto>();
            foreach (var status in statuses)
            {
                response.Add(new ExpenseStatusDto
                {
                    StatusID = status.StatusID,
                    StatusName = status.StatusName,
                    LastUpdated = status.LastUpdated
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetExpenseStatusByID([FromRoute] int id)
        {
            var existingStatus = await expenseStatusRepository.GetByIdAsync(id);
            if (existingStatus == null)
            {
                return NotFound();
            }
            var response = new ExpenseStatusDto
            {
                StatusID = existingStatus.StatusID,
                StatusName = existingStatus.StatusName,
                LastUpdated = existingStatus.LastUpdated
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditExpenseStatus([FromRoute] int id, EditExpenseStatusDto editExpenseStatus)
        {
            var editStatus = new ExpenseStatus
            {
                StatusID = id,
                StatusName = editExpenseStatus.StatusName,
                LastUpdated = editExpenseStatus.LastUpdated
            };
            editStatus = await expenseStatusRepository.UpdateAsync(editStatus);
            if (editStatus == null)
            {
                return NotFound();
            }
            var response = new ExpenseStatusDto
            {
                StatusID = editStatus.StatusID,
                StatusName = editStatus.StatusName,
                LastUpdated = editStatus.LastUpdated
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteExpenseStatus([FromRoute] int id)
        {
            var existingStatus = await expenseStatusRepository.DeleteAsync(id);
            if (existingStatus == null)
            {
                return NotFound();
            }
            var response = new ExpenseStatusDto
            {
                StatusID = existingStatus.StatusID,
                StatusName = existingStatus.StatusName,
                LastUpdated = existingStatus.LastUpdated
            };
            return Ok(response);
        }
    }
}
