using System.ComponentModel.DataAnnotations;
using Med_App_API.Models;

namespace Med_App_API.Dto
{
    public class UserForRegisterDto
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] public string UserName { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "You must specify between 8 and 16")]
        public string Password { get; set; }

    }
}