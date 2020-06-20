using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Med_App_API.Data;
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
        private readonly DataContext _context;
        private readonly IAuthRepository _authRepository;
        private readonly IMedicalRepository _medicalRepository;
        private readonly UserManager<User> _userManager;

        public GdprController(DataContext context, IAuthRepository authRepository, IMedicalRepository medicalRepository,
            UserManager<User> userManager)
        {
            _context = context;
            _authRepository = authRepository;
            _medicalRepository = medicalRepository;
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
            {
                return NoContent();
            }

            throw new Exception($"Something went wrong deleting user {id}");
        }
    }
}