using System.Collections.Generic;
using System.Linq;
using Med_App_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace Med_App_API.Data
{
    public class Seed
    {
        public static void SeedUsersAndRoles(UserManager<User> userManager, RoleManager<Role> roleManager, DataContext context)
                                             
        {
            if (!EnumerableExtensions.Any(userManager.Users))
            {
                var patientsData = System.IO.File.ReadAllText("Data/PatientSeedData.json");
                var physicianData = System.IO.File.ReadAllText("Data/PhysicianSeedData.json");
                var vaccinesData = System.IO.File.ReadAllText("Data/VaccinesSeedData.json");
                var patients = JsonConvert.DeserializeObject<List<User>>(patientsData);
                var physicians = JsonConvert.DeserializeObject<List<User>>(physicianData);
                var vaccineses = JsonConvert.DeserializeObject<List<Vaccines>>(physicianData);

                var roles = new List<Role>
                {
                    new Role {Name = "Admin"},
                    new Role {Name = "Physician"},
                    new Role {Name = "Patient"},
                };
                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                foreach (var patient in patients)
                {
                    userManager.CreateAsync(patient, "password").Wait();
                    userManager.AddToRoleAsync(patient, "Patient").Wait();
                    var addPatient = new Patient
                        {UserFKId = patient.Id, FullName = $"{patient.FirstName} {patient.LastName}"};
                    context.Patients.AddAsync(addPatient);
                    
                }
                foreach (var physician in physicians)
                {
                    userManager.CreateAsync(physician, "password").Wait();
                    userManager.AddToRoleAsync(physician, "Physician").Wait();
                    var physicianRole = new Physician
                        {UserFKId = physician.Id, FullName = $"{physician.FirstName} {physician.LastName}"};
                    context.Physicians.AddAsync(physicianRole);
                }

                // foreach (var vaccinese in vaccineses)
                // {
                //     var vaccinesAdd = new Vaccines
                //     {
                //         Name = vaccinese.Name
                //     };
                //     context.Vaccineses.AddAsync(vaccinesAdd);
                // }
                
                context.SaveChanges();

                var adminUser = new User
                {
                    Email = "admin@example.com",
                    UserName = "Admin",
                    FirstName = "Admin",
                    LastName = "Admin",
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