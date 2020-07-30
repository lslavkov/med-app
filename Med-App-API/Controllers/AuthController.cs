using System;
using System.Linq;
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

        public AuthController(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager,
                              IMapper mapper, IAuthRepository authRepository, DataContext dataContext)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _authRepository = authRepository;
            _dataContext = dataContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserForRegisterDto userForRegisterDto)
        {
            // userForRegisterDto.UserName =
            //     _authRepository.GenerateUserName(userForRegisterDto.FirstName, userForRegisterDto.LastName);

            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            if (result.Succeeded)
            {
                await _authRepository.GenerateConfirmEmail(userToCreate);

                if (userForRegisterDto.Email.EndsWith("@utb.cz"))
                {
                    var user = _userManager.FindByEmailAsync(userForRegisterDto.Email).Result;
                    var userToPhysician = _userManager.AddToRoleAsync(user, "Physician").Result;
                    if (userToPhysician.Succeeded)
                    {
                        var physician = new Physician {UserFKId = user.Id};
                        await _dataContext.Physicians.AddAsync(physician);
                    }
                }
                else
                {
                    var user = _userManager.FindByEmailAsync(userForRegisterDto.Email).Result;
                    var userToPatient = _userManager.AddToRoleAsync(user, "Patient").Result;
                    if (userToPatient.Succeeded)
                    {
                        var patient = new Patient {UserFKId = user.Id};
                        await _dataContext.Patients.AddAsync(patient);
                    }
                }

                await _dataContext.SaveChangesAsync();

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
            {
                throw new Exception("This account has not being verified. Please verify your account!");
            }


            return Unauthorized();
        }

        // [HttpGet("email/verify/password")]
        // public async Task<IActionResult> ConfirmPasswordChanging(string userid, string token)
        // {
        //     if (string.IsNullOrWhiteSpace(userid) || string.IsNullOrWhiteSpace(token))
        //         return NotFound();
        //     
        //     var result = await _authRepository.GenerateConfirmEmail(userid,token);
        //     if (result.IsSuccess)
        //     {
        //         return Redirect($"{_config["SPAUrl"]}/confirm");
        //     }
        //
        //     return BadRequest(result);
        // }

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
    }
}