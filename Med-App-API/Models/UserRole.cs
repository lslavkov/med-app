using Microsoft.AspNetCore.Identity;

namespace Med_App_API.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}