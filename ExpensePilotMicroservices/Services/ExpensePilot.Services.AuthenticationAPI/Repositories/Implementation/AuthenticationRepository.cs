using ExpensePilot.Services.AuthenticationAPI.Data;
using ExpensePilot.Services.AuthenticationAPI.Models.Domain;
using ExpensePilot.Services.AuthenticationAPI.Models.DTO;
using ExpensePilot.Services.AuthenticationAPI.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpensePilot.Services.AuthenticationAPI.Repositories.Interface
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AuthDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public AuthenticationRepository(AuthDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            User user = new()
            {
                Fname = registrationRequestDto.Fname,
                Lname = registrationRequestDto.Lname,
                UserName = registrationRequestDto.Email.Split('@')[0],
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };
            try
            {
                var result = await userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == registrationRequestDto.Email);

                    AuthUserDto userDto = new()
                    {
                        ID = userToReturn.Id,
                        Fname = userToReturn.Fname,
                        Lname = userToReturn.Lname,
                        Email = userToReturn.Email,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "User registered Successfully";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex) 
            {

            }
            return "User Registered but with errors. Please check with the Administrator";
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u=>u.UserName.ToLower()==loginRequestDto.UserName.ToLower());
            bool isValid = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if(user == null || isValid == false)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }
            //if User found - Generate Token
            var token = jwtTokenGenerator.GenerateToken(user);
            AuthUserDto userDto = new()
            {
                ID = user.Id,
                Fname = user.Fname,
                Lname = user.Lname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }
    }
}
