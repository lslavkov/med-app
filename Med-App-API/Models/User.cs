using Microsoft.AspNetCore.Identity;

namespace Med_App_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsVerifiedRegistration { get; set; }
        public bool IsVerifiedPassword { get; set; }
        public User()
        {
            IsVerifiedRegistration = false;
            IsVerifiedPassword = false;
        }
    }
}