using System.Linq;
using System.Threading.Tasks;
using Med_App_API.Data;
using Med_App_API.Data.Interface;
using Med_App_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Med_App_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMedicalRepository _repo;
        private readonly UserManager<User> _userManager;

        public AdminController(DataContext context, IMedicalRepository repo, UserManager<User> userManager)
        {
            _context = context;
            _repo = repo;
            _userManager = userManager;
        }

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await _context.Users.OrderBy(x => x.UserName).Select(user => new
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = (from userRole in user.UserRoles
                    join role in _context.Roles on userRole.RoleId equals role.Id
                    select role.Name).ToList()
            }).ToListAsync();
            return Ok(userList);
        }

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpGet("getAppointments")]
        public async Task<IActionResult> GetAppointments()
        {
            var appointmentList = await _context.Appointments.OrderBy(u => u.StartOfAppointment).Select(appointment =>
                new
                {
                    Id = appointment.Id,
                    PhysicianFullName = appointment.PhysicianFullName,
                    PatientFullName = appointment.PatientFullName,
                    StartOfAppointment = appointment.StartOfAppointment,
                    EndOfAppointment = appointment.EndOfAppointment,
                    Description = appointment.Description
                }).ToListAsync();

            return Ok(appointmentList);
        }

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpDelete("delete/user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repo.GetUser(id);

            var result = _userManager.DeleteAsync(user).Result;
            
            if (result.Succeeded)
                return Ok();
            
            return BadRequest();
        }
    }
}