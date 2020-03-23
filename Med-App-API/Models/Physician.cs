using System.Collections.Generic;

namespace Med_App_API.Models
{
    public class Physician
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
    }
}