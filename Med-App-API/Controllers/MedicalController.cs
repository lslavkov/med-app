using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Med_App_API.Data;
using Med_App_API.Dto;
using Med_App_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Med_App_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalController : ControllerBase
    {
        private readonly IMedicalRepository _repo;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public MedicalController(IMedicalRepository repo, IMapper mapper, DataContext context,
                                 UserManager<User> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("get/upcoming/appointments")]
        public async Task<IActionResult> GetUpcomingAppointments()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = _repo.GetUser(currentUserId);

            if (await _userManager.IsInRoleAsync(user.Result, "Patient"))
            {
                var patientId = await _repo.GetPatient(currentUserId);

                var appointments = await _repo.GetPatientsAppointments(patientId.Id);

                var appointmentsToReturn = _mapper.Map<IEnumerable<AppointmentPatientForListDto>>(appointments);

                return Ok(appointmentsToReturn);
            }

            if (await _userManager.IsInRoleAsync(user.Result, "Physician"))
            {
                var physicianId = await _repo.GetPhysician(currentUserId);

                var appointments = await _repo.GetPhysicianAppointments(physicianId.Id);

                var appointmentsToReturn = _mapper.Map<IEnumerable<AppointmentPhysicianForListDto>>(appointments);

                return Ok(appointmentsToReturn);
            }

            return BadRequest();
        }

        [HttpPost("create/user/{id}/appointment")]
        public async Task<IActionResult> CreatePatientAppointment(int id, AppointmentForCreatingDto model)
        {
            bool isTrue = CheckTimeRange(model.StartOfAppointment);
            if (isTrue)
                throw new Exception("Invalid time range");
            var user = await _repo.GetUser(id);
            if (user.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.UserFKId == user.Id);
            var date = model.StartOfAppointment;
            var compare = _context.Appointments
                                  .Where(d => d.StartOfAppointment <= date && d.EndOfAppointment > date)
                                  .ToList();
            if (compare.Count >= 1)
                throw new Exception("Please choose new time for appointment, this time is taken");
            model.PatientFKId = patient.Id;
            model.EndOfAppointment = model.StartOfAppointment.AddMinutes(15);

            var physicianId = model.PhysicianFKId;

            var physicianUser = await _repo.GetPhysician(physicianId);
            var userPhysician = await _repo.GetUser(physicianUser.UserFKId);

            model.PatientFullName = user.FirstName + " " + user.LastName;
            model.PhysicianFullName = userPhysician.FirstName + " " + userPhysician.LastName;

            var appointment = _mapper.Map<Appointment>(model);

            _repo.Add(appointment);

            if (await _repo.SaveAll())
                return NoContent();
            
            throw new Exception("Failed on creating new appointment");
        }

        [HttpGet("get/appointment/{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appointment = await _repo.GetAppointment(id);
            var appointmentToReturn = _mapper.Map<AppointmentPatientForListDto>(appointment);

            return Ok(appointmentToReturn);
        }

        [HttpDelete("delete/appointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _repo.GetAppointment(id);

            _repo.Delete(appointment);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Error while deleting appointment's id {id}");
        }

        private bool CheckTimeRange(DateTime time)
        {
            TimeSpan startShift = new TimeSpan(8, 0, 0);
            TimeSpan endShift = new TimeSpan(14, 0, 0);
            TimeSpan check = time.TimeOfDay;

            if (check < startShift || endShift < check)
                return true;

            return false;
        }
    }
}