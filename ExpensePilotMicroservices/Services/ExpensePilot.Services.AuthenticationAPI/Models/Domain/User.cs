using Microsoft.AspNetCore.Identity;

namespace ExpensePilot.Services.AuthenticationAPI.Models.Domain
{
    public class User : IdentityUser
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string? ManagerId { get; set; }
        public int? DepartmentId { get; set; }

        //Navigation Properties
        public User Manager { get; set; }
        public ICollection<User> Subordinates { get; set; }
    }
}
