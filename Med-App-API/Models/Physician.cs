using System.Collections.Generic;

namespace Med_App_API.Models
{
    public class Physician : User
    {
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
        public ICollection<Workday> Workdays { get; set; }
        public Physician() : base()
        {
            
        }
    }
}