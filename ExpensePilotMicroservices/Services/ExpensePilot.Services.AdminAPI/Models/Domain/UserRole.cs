using Microsoft.AspNetCore.Identity;

namespace ExpensePilot.Services.AuthenticationAPI.Models.Domain
{
    public class UserRole:IdentityUserRole<Guid>
    {
    }
}
