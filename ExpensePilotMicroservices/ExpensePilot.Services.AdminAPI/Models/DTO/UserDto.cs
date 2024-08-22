namespace ExpensePilot.Services.AuthenticationAPI.Models.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? ManagerId { get; set; }
        public string ManagerName {  get; set; }
        //public int? DepartmentId { get; set; }

    }
}
