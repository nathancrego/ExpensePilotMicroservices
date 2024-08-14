namespace ExpensePilot.Services.AuthenticationAPI.Models.DTO
{
    public class RegistrationRequestDto
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
