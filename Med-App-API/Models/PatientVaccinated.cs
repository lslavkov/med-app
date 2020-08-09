using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Med_App_API.Models
{
    public class PatientVaccinated
    {
        public int Id { get; set; }
        public virtual Patient Patient { get; set; }
        public Vaccines Vaccines { get; set; }
        [ForeignKey("Patient")] public int PatientFKId { get; set; }
        [ForeignKey("Vaccines")] public int VacinesFKId { get; set; }
        public DateTime TimeOfVaccination { get; set; }
        public double DosageMl { get; set; }
        public string? Description { get; set; }
    }
}