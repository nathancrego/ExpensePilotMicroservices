using ExpensePilot.Services.AdminAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace ExpensePilot.Services.AuthenticationAPI.Models.DTO
{
    public class AddUserDto
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? ManagerId { get; set; }
        public int? DepartmentId { get; set; }
        public RoleDto Role { get; set; }
    }
}
