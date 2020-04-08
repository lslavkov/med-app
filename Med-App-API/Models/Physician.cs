using System.Collections.Generic;

namespace Med_App_API.Models
{
    public class Physician
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsVerifiedRegistration { get; set; }
        public bool IsVerifiedPassword { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
        public ICollection<Workday> Workdays { get; set; }
        public Physician()
        {
            IsVerifiedRegistration = false;
            IsVerifiedPassword = false;
        }
    }
}