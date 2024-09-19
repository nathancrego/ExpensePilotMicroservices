using ExpensePilot.Services.AdminAPI.Models.Domain;
using ExpensePilot.Services.AdminAPI.Models.DTO;
using ExpensePilot.Services.AdminAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePilot.Services.AdminAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        //POST:
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(AddDepartmentDto addDepartment)
        {
            //Convert Dto to Domain model
            var adddpt = new Department
            {
                DepartmentName = addDepartment.DepartmentName,
                LastUpdated = addDepartment.LastUpdated
            };
            await departmentRepository.CreateAsync(adddpt);

            //Convert Domain Model to DTO
            var response = new DepartmentDto
            {
                DptID = adddpt.DptID,
                DepartmentName = adddpt.DepartmentName,
                LastUpdated = adddpt.LastUpdated
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await departmentRepository.GetAllAsync();
            var response = new List<DepartmentDto>();
            foreach (var department in departments)
            {
                response.Add(new DepartmentDto
                {
                    DptID = department.DptID,
                    DepartmentName = department.DepartmentName,
                    LastUpdated = department.LastUpdated
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetDepartmentByID([FromRoute] int id)
        {
            var existingDepartment = await departmentRepository.GetByIdAsync(id);
            if (existingDepartment == null)
            {
                return NotFound();
            }
            var response = new DepartmentDto
            {
                DptID = existingDepartment.DptID,
                DepartmentName = existingDepartment.DepartmentName,
                LastUpdated = existingDepartment.LastUpdated
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditDepartment([FromRoute] int id, EditDepartmentDto editDepartment)
        {
            var editdpt = new Department
            {
                DptID = id,
                DepartmentName = editDepartment.DepartmentName,
                LastUpdated = editDepartment.LastUpdated
            };
            editdpt = await departmentRepository.UpdateAsync(editdpt);
            if (editdpt is null)
            {
                return NotFound();
            }
            var response = new DepartmentDto
            {
                DptID = editdpt.DptID,
                DepartmentName = editdpt.DepartmentName,
                LastUpdated = editdpt.LastUpdated
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            var existingDepartment = await departmentRepository.DeleteAsync(id);
            if (existingDepartment is null)
            {
                return NotFound();
            }
            var response = new DepartmentDto
            {
                DptID = existingDepartment.DptID,
                DepartmentName = existingDepartment.DepartmentName,
                LastUpdated = existingDepartment.LastUpdated
            };
            return Ok(response);
        }
    }
}
