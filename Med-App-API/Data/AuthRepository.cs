using System.Threading.Tasks;
using Med_App_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Med_App_API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Physician> PhysicianLogin(string email, string password)
        {
            var physician = await _context.Physician.FirstOrDefaultAsync(x => x.Email == email);

            if (physician == null)
                return null;

            if (!VerifyPasswordHash(password, physician.PasswordHash, physician.PasswordSalt))
                return null;

            return physician;
        }

        public async Task<Patient> PatientLogin(string email, string password)
        {
            var patient = await _context.Patient.FirstOrDefaultAsync(x => x.Email == email);
            if (patient == null)
                return null;

            if (!VerifyPasswordHash(password, patient.PasswordHash, patient.PasswordSalt))
                return null;

            return patient;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }

        public async Task<Physician> RegisterPhysician(Physician physician, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            physician.PasswordHash = passwordHash;
            physician.PasswordSalt = passwordSalt;

            await _context.Physician.AddAsync(physician);
            await _context.SaveChangesAsync();

            return physician;
        }

        public async Task<Patient> RegisterPatient(Patient patient, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            patient.PasswordHash = passwordHash;
            patient.PasswordSalt = passwordSalt;

            await _context.Patient.AddAsync(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Physician.AnyAsync(x => x.Email == email) ||
                await _context.Patient.AnyAsync(x => x.Email == email))
                return true;

            return false;
        }
    }
}