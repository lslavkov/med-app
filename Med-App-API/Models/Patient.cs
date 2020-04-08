using System.Collections.Generic;

namespace Med_App_API.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsVerifiedRegistration { get; set; }
        public bool IsVerifiedPassword { get; set; }
        public int PhysicianId { get; set; }
        public Physician Physician { get; set; }
        public ICollection<Appointments> Appointments { get; set; }

        public Patient()
        {
            IsVerifiedRegistration = false;
            IsVerifiedPassword = false;
        }
    }
}