using System.Collections.Generic;
using System.Threading.Tasks;
using Med_App_API.Models;

namespace Med_App_API.Data
{
    public interface IMedicalRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<User> GetUser(int id);
        Task<Patient> GetPatient(int id);
        Task<Physician> GetPhysician(int id);
        Task<IEnumerable<Appointment>> GetPatientsAppointments(int id);
        Task<IEnumerable<Appointment>> GetPhysicianAppointments(int id);
        Task<Appointment> GetAppointment(int id);
    }
}