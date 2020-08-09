using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Med_App_API.Data;
using Med_App_API.Data.Interface;
using Med_App_API.Dto;
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
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;

        public AdminController(DataContext context, IMedicalRepository repo, UserManager<User> userManager,
                               IMapper mapper, IAuthRepository authRepository)
        {
            _context = context;
            _repo = repo;
            _userManager = userManager;
            _mapper = mapper;
            _authRepository = authRepository;
        }


        [Authorize(Policy = "RequiredAdminRole")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserForRegisterDto userForRegisterDto)
        {
            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var generatePassword = PasswordGenerator();
            var result = await _userManager.CreateAsync(userToCreate, generatePassword);

            if (result.Succeeded)
            {
                await _authRepository.GenerateConfirmEmail(userToCreate, generatePassword);

                var user = _userManager.FindByEmailAsync(userForRegisterDto.Email).Result;
                var userToPhysician = _userManager.AddToRoleAsync(user, "Physician").Result;
                if (userToPhysician.Succeeded)
                {
                    var physician = new Physician {UserFKId = user.Id, FullName = $"{user.FirstName} {user.LastName}"};
                    _repo.Add(physician);
                }

                if (await _repo.SaveAll())
                    return Ok();
            }

            return BadRequest();
        }
        [Authorize(Policy = "RequiredAdminRole")]
        [HttpPost("create/vaccine")]
        public async Task<IActionResult> CreateVaccine(VaccineForCreationDto model)
        {
            var vaccineToCreate = _mapper.Map<Vaccines>(model);
            
            _repo.Add(vaccineToCreate);
            
            if (await _repo.SaveAll())
                return NoContent();
            
            return BadRequest();
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
                    Description = appointment.Description,
                    TypeOfAppointment = appointment.TypeOfAppointment
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
        private string PasswordGenerator()
        {
            var builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}