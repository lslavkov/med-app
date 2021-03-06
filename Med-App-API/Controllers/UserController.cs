using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Med_App_API.Data;
using Med_App_API.Data.Interface;
using Med_App_API.Dto;
using Med_App_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Med_App_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMedicalRepository _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserController(IMedicalRepository repo, IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("change/{id}")]
        public async Task<IActionResult> UpdateUserInfo(int id, [FromBody] UserForUpdateDto model)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);
            _mapper.Map(model, userFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpPost("change/password")]
        public async Task<IActionResult> ChangePassword(UserForNewPasswordDto model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
                return Ok();
            return BadRequest(result);
        }

        [HttpGet("get/physicians")]
        public async Task<IActionResult> GetPhysicians()
        {
            var physicians = await _repo.GetPhysicians();

            var physiciansToReturn = _mapper.Map<IEnumerable<PhysicianForListDto>>(physicians);
            
            return Ok(physiciansToReturn);
        }

        [HttpGet("get/patients")]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _repo.GetPatients();

            var patientsToReturn = _mapper.Map<IEnumerable<PatientForListDto>>(patients);

            return Ok(patientsToReturn);
        }

        [HttpDelete("delete/{id}")]
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