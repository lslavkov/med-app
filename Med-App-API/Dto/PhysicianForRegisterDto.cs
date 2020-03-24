using System.ComponentModel.DataAnnotations;

namespace Med_App_API.Dto
{
    public class PhysicianForRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
        //[Required]
        public string FirstName { get; set; }
        //[Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}