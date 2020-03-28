using System.Collections.Generic;

namespace Med_App_API.Models
{
    public class Patient : User
    {
        public int PhysicianId { get; set; }
        public Physician Physician { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
        public Patient() : base()
        {

        }


    }
}