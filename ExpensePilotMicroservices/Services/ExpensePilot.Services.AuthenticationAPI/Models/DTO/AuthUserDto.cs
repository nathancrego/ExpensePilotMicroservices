namespace ExpensePilot.Services.AuthenticationAPI.Models.DTO
{
    public class AuthUserDto
    {
        public Guid ID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
    }
}
