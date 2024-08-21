﻿using ExpensePilot.Services.AuthenticationAPI.Models.Domain;
using ExpensePilot.Services.AuthenticationAPI.Models.DTO;
using ExpensePilot.Services.AuthenticationAPI.Repositories.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePilot.Services.AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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
                PhoneNumber = addUser.PhoneNumber,
                ManagerId = addUser.ManagerId
            };
            await userRepository.CreateAsync(add);

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
                ManagerName = add.Manager != null ? $"{add.Manager.Fname}{add.Manager.Lname}" : null
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
                response.Add(new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Fname = user.Fname,
                    Lname = user.Lname,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    ManagerId = user.ManagerId,
                    ManagerName = user.Manager != null ? $"{user.Manager.Fname}{user.Manager.Lname}" : null
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

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> EditUser([FromRoute] Guid id, EditUserDto editUser)
        {
            var edit = new User
            {
                Id = id,
                Fname = editUser.Fname,
                Lname = editUser.Lname,
                Email = editUser.Email,
                NormalizedEmail = editUser.Email.ToUpper(),
                UserName = editUser.Email.Split('@')[0],
                NormalizedUserName = editUser.Email.Split('@')[0].ToUpper(),
                PhoneNumber = editUser.PhoneNumber,
                ManagerId = editUser.ManagerId
            };
            edit = await userRepository.UpdateAsync(edit);
            if(edit is null)
            {
                return NotFound();
            }
            var response = new UserDto
            {
                Id = edit.Id,
                UserName = edit.UserName,
                Fname = edit.Fname,
                Lname = edit.Lname,
                Email = edit.Email,
                PhoneNumber = edit.PhoneNumber,
                ManagerId = edit.ManagerId,
                ManagerName = edit.Manager != null ? $"{edit.Manager.Fname}{edit.Manager.Lname}" : null
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
