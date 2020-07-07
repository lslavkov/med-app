using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Med_App_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Med_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class GdprController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public GdprController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpDelete("user/delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            string idString = id.ToString();
            var user = await _userManager.FindByIdAsync(idString);
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return NoContent();

            throw new Exception($"Something went wrong deleting user {id}");
        }
    }
}