using ExpensePilot.API.Models.DTO.Policies;
using ExpensePilot.API.Repositories.Interface.Policies;
using ExpensePilot.Services.PoliciesAPI.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePilot.API.Controllers.Policies
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly IPolicyRepository policyRepository;

        public PoliciesController(IPolicyRepository policyRepository)
        {
            this.policyRepository = policyRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePolicy(AddPolicyDto addPolicy)
        {
            var addp = new Policy
            {
                PolicyName = addPolicy.PolicyName,
                PolicyPurpose = addPolicy.PolicyPurpose,
                PolicyDescription = addPolicy.PolicyDescription,
                LastUpdated = addPolicy.LastUpdated,
            };
            await policyRepository.CreateAsync(addp);

            var response = new PolicyDto
            {
                PolicyID = addp.PolicyID,
                PolicyName = addp.PolicyName,
                PolicyPurpose = addp.PolicyPurpose,
                PolicyDescription = addp.PolicyDescription,
                LastUpdated = addp.LastUpdated,
            };
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPolicy()
        {
            var policies = await policyRepository.GetAllAsync();
            var response = new List<PolicyDto>();
            foreach (var policy in policies)
            {
                response.Add(new PolicyDto
                {
                    PolicyID = policy.PolicyID,
                    PolicyName = policy.PolicyName,
                    PolicyPurpose = policy.PolicyPurpose,
                    PolicyDescription = policy.PolicyDescription,
                    LastUpdated = policy.LastUpdated,
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetPolicyByID([FromRoute] int id)
        {
            var existingPolicy = await policyRepository.GetIDAsync(id);
            if (existingPolicy == null)
            {
                return NotFound();
            }
            var response = new PolicyDto
            {
                PolicyID = existingPolicy.PolicyID,
                PolicyName = existingPolicy.PolicyName,
                PolicyPurpose = existingPolicy.PolicyPurpose,
                PolicyDescription = existingPolicy.PolicyDescription,
                LastUpdated = existingPolicy.LastUpdated,
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditPolicy([FromRoute] int id, EditPolicyDto editPolicy)
        {
            var editp = new Policy
            {
                PolicyID = id,
                PolicyName = editPolicy.PolicyName,
                PolicyPurpose = editPolicy.PolicyPurpose,
                PolicyDescription = editPolicy.PolicyDescription,
                LastUpdated = editPolicy.LastUpdated,
            };
            editp = await policyRepository.UpdateAsync(editp);
            if (editp == null)
            {
                return NotFound();
            }
            var response = new PolicyDto
            {
                PolicyID = editp.PolicyID,
                PolicyName = editp.PolicyName,
                PolicyPurpose = editp.PolicyPurpose,
                PolicyDescription = editp.PolicyDescription,
                LastUpdated = editp.LastUpdated,
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeletePolicy([FromRoute] int id)
        {
            var existingPolicy = await policyRepository.DeleteAsync(id);
            if (existingPolicy == null)
            {
                return NotFound();
            }
            var response = new PolicyDto
            {
                PolicyID = existingPolicy.PolicyID,
                PolicyName = existingPolicy.PolicyName,
                PolicyPurpose = existingPolicy.PolicyPurpose,
                PolicyDescription = existingPolicy.PolicyDescription,
                LastUpdated = existingPolicy.LastUpdated,
            };
            return Ok(response);
        }

    }
}
