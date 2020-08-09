using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Med_App_API.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("User")] public int UserFKId { get; set; }
        public string FullName { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
        public virtual ICollection<PatientVaccinated> Vaccineses { get; set; }
    }
}