using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Med_App_API.Data;
using Med_App_API.Dto;
using Med_App_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Med_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register/patient")]
        public async Task<IActionResult> RegisterPatient(PatientForRegisterDto patientForRegisterDto)
        {
            if (await _repo.UserExists(patientForRegisterDto.Email))
                return BadRequest("Email already exists");

            var patientToCreate = new Patient
            {
                Email = patientForRegisterDto.Email,
                FirstName = patientForRegisterDto.FirstName,
                LastName = patientForRegisterDto.LastName
            };

            var createdPatient = await _repo.RegisterPatient(patientToCreate, patientForRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("register/physician")]
        public async Task<IActionResult> RegisterPhysician(PhysicianForRegisterDto physicianForRegisterDtop)
        {
            if (await _repo.UserExists(physicianForRegisterDtop.Email))
                return BadRequest("Email already exists");

            var physicianToCreate = new Physician
            {
                Email = physicianForRegisterDtop.Email,
                FirstName = physicianForRegisterDtop.FirstName,
                LastName = physicianForRegisterDtop.LastName
            };

            var createdPhysician = await _repo.RegisterPhysician(physicianToCreate, physicianForRegisterDtop.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin(UserForLoginDto userForLoginDto)
        {
            var physicianFromRepo = await _repo.PhysicianLogin(userForLoginDto.Email, userForLoginDto.Password);
            var patientFromRepo = await _repo.PatientLogin(userForLoginDto.Email, userForLoginDto.Password);
            var claims = new Claim[2];

            if (physicianFromRepo != null)
            {
                claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, physicianFromRepo.Email),
                    new Claim(ClaimTypes.Email, physicianFromRepo.Email),
                };
            }
            else if (patientFromRepo != null)
            {
                claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, patientFromRepo.Email),
                    new Claim(ClaimTypes.Email, patientFromRepo.Email),
                };
            }
            else
            {
                return BadRequest();
            }
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
            
        }

        [HttpPost("login/physician")]
        public async Task<IActionResult> LoginPhysician(PhysicianForLoginDto physicianForLoginDto)
        {
            var physicianFromRepo =
                await _repo.PhysicianLogin(physicianForLoginDto.Email, physicianForLoginDto.Password);

            if (physicianFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, physicianFromRepo.Email),
                new Claim(ClaimTypes.Email, physicianFromRepo.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }

        [HttpPost("login/patient")]
        public async Task<IActionResult> LoginPatient(PatientForLoginDto patientForLoginDto)
        {
            var physicianFromRepo = await _repo.PatientLogin(patientForLoginDto.Email, patientForLoginDto.Password);

            if (physicianFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, physicianFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Email, physicianFromRepo.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}