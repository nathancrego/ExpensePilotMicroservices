using System.ComponentModel.DataAnnotations;

namespace ExpensePilot.Services.AdminAPI.Models.DTO
{
    public class EditExpenseCategoryDto
    {
        [Required]
        public string CategoryName { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
