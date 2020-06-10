using System.Collections.Generic;
using Med_App_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace Med_App_API.Data
{
    public class Seed
    {
        public static void SeedUsersAndRoles(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var roles = new List<Role>
                {
                    new Role {Name = "Admin"},
                    new Role {Name = "Physician"},
                    new Role {Name = "Nurse"},
                    new Role {Name = "Patient"},
                };
                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                var adminUser = new User
                {
                    Email = "admin@example.com",
                    UserName = "Admin",
                    FirstName = "Admin",
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(adminUser, "password").Result;

                if (result.Succeeded)
                {
                    var admin = userManager.FindByNameAsync("Admin").Result;
                    var adminRole = userManager.AddToRoleAsync(admin, "Admin").Result;
                }
            }
        }
    }
}