using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Med_App_API.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        // public ICollection<Patient> Patients { get; set; }
        // public ICollection<Physician> Physicians { get; set; }
    }
}