using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Med_App_API.Models
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}