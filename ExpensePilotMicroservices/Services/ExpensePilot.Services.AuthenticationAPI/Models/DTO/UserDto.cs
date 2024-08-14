namespace ExpensePilot.Services.AuthenticationAPI.Models.DTO
{
    public class UserDto
    {
        public string ID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ManagerID { get; set; }
        public string ManagerName { get; set; }
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
}
