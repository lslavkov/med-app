using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Med_App_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Med_App_API.Data
{
    public class MedicalRepository : IMedicalRepository
    {
        private readonly DataContext _context;

        public MedicalRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<Patient> GetPatient(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(u => u.UserFKId == id);

            return patient;
        }

        public async Task<Physician> GetPhysician(int id)
        {
            var physician = await _context.Physicians.FirstOrDefaultAsync(u => u.Id == id);

            return physician;
        }
        public async Task<Appointment> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            return appointment;
        }

        public async Task<IEnumerable<Appointment>> GetPatientsAppointments(int id)
        {
            var patientAppointments =
                await _context.Appointments
                              .Where(p => p.PatientFKId == id && p.StartOfAppointment > DateTime.Now)
                              .OrderByDescending(d => d.StartOfAppointment)
                              .ToListAsync();

            return patientAppointments;
        }
        
        public async Task<IEnumerable<Appointment>> GetPhysicianAppointments(int id)
        {
            var patientAppointments =
                await _context.Appointments
                              .Where(p => p.PatientFKId == id && p.StartOfAppointment > DateTime.Now)
                              .OrderByDescending(d => d.StartOfAppointment)
                              .ToListAsync();

            return patientAppointments;
        }
        
        
    }
}