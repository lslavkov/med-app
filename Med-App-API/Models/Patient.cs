using System.Collections.Generic;

namespace Med_App_API.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PhysicianId { get; set; }
        public Physician Physician { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
        
    }
}