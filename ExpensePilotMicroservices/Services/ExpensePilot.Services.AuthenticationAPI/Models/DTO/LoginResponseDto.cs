namespace ExpensePilot.Services.AuthenticationAPI.Models.DTO
{
    public class LoginResponseDto
    {
        public AuthUserDto User { get; set; }
        public string Token { get; set; }
    }
}
