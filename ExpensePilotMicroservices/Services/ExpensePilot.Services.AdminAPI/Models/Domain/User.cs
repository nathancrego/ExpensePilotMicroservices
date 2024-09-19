using ExpensePilot.Services.AdminAPI.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace ExpensePilot.Services.AuthenticationAPI.Models.Domain
{
    public class User : IdentityUser<Guid>
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public Guid? ManagerId { get; set; }
        public int? DepartmentId { get; set; }

        //Navigation Properties
        public Department Department { get; set; }
        public User Manager { get; set; }
        public ICollection<User> Subordinates { get; set; } = new List<User>();
    }
}
