using System.ComponentModel.DataAnnotations;

namespace Med_App_API.Dto
{
    public class UserForRegisterDto
    {
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }
        [Required] public string UserName { get; set; }

    }
}