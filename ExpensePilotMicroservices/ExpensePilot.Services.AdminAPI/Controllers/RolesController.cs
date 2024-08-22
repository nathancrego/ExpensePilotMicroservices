using ExpensePilot.Services.AdminAPI.Models.DTO;
using ExpensePilot.Services.AdminAPI.Repositories.Interface;
using ExpensePilot.Services.AuthenticationAPI.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePilot.Services.AdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(AddRoleDto addRole)
        {
            var add = new Role
            {
                Name = addRole.RoleName,
                NormalizedName = addRole.RoleName.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            await roleRepository.CreateAsync(add);
            var response = new RoleDto
            {
                Id = add.Id,
                RoleName = add.Name
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await roleRepository.GetAllAsync();
            var response = new List<RoleDto>();
            foreach (var role in roles)
            {
                response.Add(new RoleDto
                {
                    Id = role.Id,
                    RoleName = role.Name
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRoleById([FromRoute] Guid id)
        {
            var existingRole = await roleRepository.GetByIdAsync(id);
            if (existingRole == null)
            {
                return NotFound("The role doesn't exist");
            }
            var response = new RoleDto
            {
                Id = existingRole.Id,
                RoleName = existingRole.Name
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> EditRole([FromRoute] Guid id, EditRoleDto editRole)
        {
            var edit = new Role
            {
                Id = id,
                Name = editRole.RoleName,
                NormalizedName = editRole.RoleName.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            edit = await roleRepository.UpdateAsync(edit);
            if (edit == null)
            {
                return NotFound("The role doesn't exist");
            }
            var response = new RoleDto
            {
                Id = edit.Id,
                RoleName = edit.Name,
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid id)
        {
            var existingRole = await roleRepository.DeleteAsync(id);
            if (existingRole == null)
            {
                return NotFound("The role doesn't exist");
            }
            var response = new RoleDto
            {
                Id = existingRole.Id,
                RoleName = existingRole.Name
            };
            return Ok(response);
        }
    }
}
