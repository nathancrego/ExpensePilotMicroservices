using ExpensePilot.Services.AuthenticationAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace ExpensePilot.Services.AdminAPI.Models.Domain
{
    public class Department
    {
        [Key]
        public int DptID { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
