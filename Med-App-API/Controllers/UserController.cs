using System.Threading.Tasks;
using AutoMapper;
using Med_App_API.Data;
using Med_App_API.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Med_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMedicalRepository _repo;
        private readonly IMapper _mapper;
        public UserController(IMedicalRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }
    }
}