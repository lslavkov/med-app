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
using Microsoft.Extensions.Configuration;

namespace Med_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        private readonly DataContext _dataContext;
        private readonly IMedicalRepository _repo;

        public AuthController(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager,
                              IMapper mapper, IAuthRepository authRepository, DataContext dataContext,
                              IMedicalRepository repo)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _authRepository = authRepository;
            _dataContext = dataContext;
            _repo = repo;
        }

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
                var userToPatient = _userManager.AddToRoleAsync(user, "Patient").Result;
                if (userToPatient.Succeeded)
                {
                    var patient = new Patient {UserFKId = user.Id, FullName = $"{user.FirstName} {user.LastName}"};
                    _repo.Add(patient);
                }

                if (await _repo.SaveAll())
                    return Ok();
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userForLoginDto.Email);
            if (user.EmailConfirmed)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

                if (result.Succeeded)
                {
                    var appUser = _mapper.Map<UserForListDto>(user);
                    var patientSearch = _dataContext.Patients.FirstOrDefault(x => x.UserFKId == appUser.Id);
                    var physicianSearch = _dataContext.Physicians.FirstOrDefault(x => x.UserFKId == appUser.Id);
                    if (user.EmailConfirmed)
                    {
                        if (patientSearch != null)
                        {
                            var appPatient = _mapper.Map<PatientForListDto>(patientSearch);
                            return Ok(new
                            {
                                token = _authRepository.GenerateJwtToken(user).Result,
                                user = appUser,
                                patient = appPatient
                            });
                        }

                        if (physicianSearch != null)
                        {
                            var appPhysician = _mapper.Map<PhysicianForListDto>(physicianSearch);
                            return Ok(new
                            {
                                token = _authRepository.GenerateJwtToken(user).Result,
                                user = appUser,
                                physician = appPhysician
                            });
                        }

                        return Ok(new
                        {
                            token = _authRepository.GenerateJwtToken(user).Result,
                            user = appUser
                        });
                    }
                }
                else
                    throw new Exception("This user has not verified e-mail. Please verify your email.");
            }

            return Unauthorized();
        }

        [HttpGet("email/verify/account")]
        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {
            if (string.IsNullOrWhiteSpace(userid) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _authRepository.ConfirmEmailAsync(userid, token);
            if (result.IsSuccess)
            {
                return Redirect($"{_config["SPAUrl"]}/confirm");
            }

            return BadRequest(result);
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