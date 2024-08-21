using ExpensePilot.Services.AuthenticationAPI.Models.DTO;

namespace ExpensePilot.Services.AuthenticationAPI.Repositories.Implementation
{
    public interface IAuthenticationRepository
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto); //we can use userdto also
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
