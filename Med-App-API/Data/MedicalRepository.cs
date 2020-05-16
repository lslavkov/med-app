using System.Threading.Tasks;
using Med_App_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Med_App_API.Data
{
    public class MedicalRepository: IMedicalRepository
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
    }
}