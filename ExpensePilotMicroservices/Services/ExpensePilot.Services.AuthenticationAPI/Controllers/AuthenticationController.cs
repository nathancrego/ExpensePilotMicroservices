using ExpensePilot.Services.AuthenticationAPI.Models.DTO;
using ExpensePilot.Services.AuthenticationAPI.Repositories.Implementation;
using ExpensePilot.Services.AuthenticationAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePilot.Services.AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository authenticationRepository;
        private readonly IPasswordResetRepository passwordResetRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository, IPasswordResetRepository passwordResetRepository)
        {
            this.authenticationRepository = authenticationRepository;
            this.passwordResetRepository = passwordResetRepository;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequest)
        {
            var registerResponse = await authenticationRepository.Register(registrationRequest);
            if(registerResponse == null)
            {
                return BadRequest("Unable to register. Please contact Administrator");
            }
            return Ok(registerResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var loginResponse = await authenticationRepository.Login(loginRequest);
            if(loginResponse.User == null)
            {
                return BadRequest("Username or Password is incorrect");
            }
            return Ok(loginResponse);
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPassword)
        {
            var user = await passwordResetRepository.FindByEmailAsync(forgotPassword.Email);
            if(user == null)
            {
                return NotFound();
            }
            var token = await passwordResetRepository.GeneratePasswordResetTokenAsync(user);
            return Ok(token);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
        {
            var user = await passwordResetRepository.FindByEmailAsync(resetPassword.Email);
            if(user == null)
            {
                return NotFound();
            }
            var result = await passwordResetRepository.ResetPasswordAsync(user, resetPassword.Token, resetPassword.NewPassword);
            if(result.Succeeded)
            {
                return Ok(new {message = "Password has be reset successfully!"});
            }
            return BadRequest();
        }
    }
}
