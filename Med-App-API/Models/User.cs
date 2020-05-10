using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Med_App_API.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}