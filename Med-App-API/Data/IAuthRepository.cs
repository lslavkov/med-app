using System.Threading.Tasks;
using Med_App_API.Models;

namespace Med_App_API.Data
{
    public interface IAuthRepository
    {
        Task<Physician> PhysicianLogin(string email, string password);
        Task<Patient> PatientLogin(string email, string password);
        Task<Physician> RegisterPhysician(Physician physician, string password);
        Task<Patient> RegisterPatient(Patient patient, string password);
        Task<bool> PhysicianExists(string email);
        Task<bool> PatientExists(string email);


    }
}