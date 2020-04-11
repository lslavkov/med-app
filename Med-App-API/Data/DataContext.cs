using Med_App_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Med_App_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // public DbSet<User> User { get; set; }
        public DbSet<Physician> Physician { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Workday> Workday { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
    }
}