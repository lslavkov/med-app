using System;
using System.ComponentModel.DataAnnotations.Schema;
using Med_App_API.Models;

namespace Med_App_API.Dto
{
    public class PatientsVaccinatesForListDto
    {
        public int Id { get; set; }
        public int PatientFKId { get; set; }
        public int VacinesFKId { get; set; }
        public DateTime TimeOfVaccination { get; set; }
        public double DosageMl { get; set; }
        public string? Description { get; set; }
    }
}