using ExpensePilot.Services.AdminAPI.Repositories.Interface;
using ExpensePilot.Services.AuthenticationAPI.Models.Domain;
using ExpensePilot.Services.AuthenticationAPI.Models.DTO;
using ExpensePilot.Services.AuthenticationAPI.Repositories.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePilot.Services.AuthenticationAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        public UsersController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AddUserDto addUser)
        {
            //Convert Dto to Domain Model
            var add = new User
            {
                Fname = addUser.Fname,
                Lname = addUser.Lname,
                Email = addUser.Email,
                NormalizedEmail = addUser.Email.ToUpper(),
                UserName = addUser.Email.Split('@')[0],
                NormalizedUserName = addUser.Email.Split('@')[0].ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                PhoneNumber = addUser.PhoneNumber,
                ManagerId = addUser.ManagerId
            };
            //string roleName = addUser.Role.RoleName;
            //await userRepository.CreateAsync(add, roleName);

            var role = await roleRepository.GetByIdAsync(addUser.Role.Id);
            var roleName = role.Name;
            await userRepository.CreateAsync(add, roleName);

            var assignedRoleName = await userRepository.GetRoleAsync(add);
            //var role = await userRepository.GetRoleAsync(add);
            //var assignedRoleName = role.FirstOrDefault();

            //Convert Domain model to DTO
            var response = new UserDto
            {
                Id = add.Id,
                UserName = add.UserName,
                Fname = add.Fname,
                Lname = add.Lname,
                Email = add.Email,
                PhoneNumber = add.PhoneNumber,
                ManagerId = add.ManagerId,
                ManagerName = add.Manager != null ? $"{add.Manager.Fname}{add.Manager.Lname}" : null,
                RoleName = assignedRoleName.FirstOrDefault()
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userRepository.GetAllAsync();
            var response = new List<UserDto>();
            foreach (var user in users)
            {
                var role = await userRepository.GetRoleAsync(user);
                response.Add(new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Fname = user.Fname,
                    Lname = user.Lname,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    ManagerId = user.ManagerId,
                    ManagerName = user.Manager != null ? $"{user.Manager.Fname}{user.Manager.Lname}" : null,
                    RoleName = role.FirstOrDefault()

                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var existingUser = await userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            var role = await userRepository.GetRoleAsync(existingUser);
            var response = new UserDto
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                Fname = existingUser.Fname,
                Lname = existingUser.Lname,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                ManagerId = existingUser.ManagerId,
                ManagerName = existingUser.Manager != null ? $"{existingUser.Manager.Fname}{existingUser.Manager.Lname}" : null,
                RoleName = role.FirstOrDefault()
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> EditUser([FromRoute] Guid id, EditUserDto editUser)
        {
            var edit = new User
            {
                Id = id,
                Fname = editUser.Fname,
                Lname = editUser.Lname,
                PhoneNumber = editUser.PhoneNumber,
                ManagerId = editUser.ManagerId
            };
            var updatedUser = await userRepository.UpdateAsync(edit, editUser.Role.RoleName);
            if(updatedUser is null)
            {
                return NotFound();
            }
            var role = await userRepository.GetRoleAsync(updatedUser);
            var response = new UserDto
            {
                Id = edit.Id,
                UserName = edit.UserName,
                Fname = edit.Fname,
                Lname = edit.Lname,
                Email = edit.Email,
                PhoneNumber = edit.PhoneNumber,
                ManagerId = edit.ManagerId,
                ManagerName = edit.Manager != null ? $"{edit.Manager.Fname}{edit.Manager.Lname}" : null,
                RoleName = role.FirstOrDefault()
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var existingUser = await userRepository.DeleteAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            var response = new UserDto
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                Fname = existingUser.Fname,
                Lname = existingUser.Lname,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                ManagerId = existingUser.ManagerId,
                ManagerName = existingUser.Manager != null ? $"{existingUser.Manager.Fname}{existingUser.Manager.Lname}" : null
            };
            return Ok(response);
        }
    }
}
