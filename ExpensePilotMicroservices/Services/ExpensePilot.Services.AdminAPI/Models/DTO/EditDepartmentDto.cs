using System.ComponentModel.DataAnnotations;

namespace ExpensePilot.Services.AdminAPI.Models.DTO
{
    public class EditDepartmentDto
    {
        [Required]
        public string DepartmentName { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
