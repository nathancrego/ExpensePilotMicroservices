using Microsoft.AspNetCore.Identity;

namespace ExpensePilot.Services.AuthenticationAPI.Models.Domain
{
    public class UserLogin:IdentityUserLogin<Guid>
    {
    }
}
