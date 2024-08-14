using ExpensePilot.Services.PoliciesAPI.Models.Domain;

namespace ExpensePilot.API.Repositories.Interface.Policies
{
    public interface IPolicyRepository
    {
        Task<Policy> CreateAsync (Policy policy);
        Task<Policy?> UpdateAsync (Policy policy);
        Task<Policy?> DeleteAsync (int id);
        Task<Policy?> GetIDAsync (int id);
        Task<IEnumerable<Policy>> GetAllAsync ();
    }
}
