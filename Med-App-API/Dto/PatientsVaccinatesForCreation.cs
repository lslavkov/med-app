using System;

namespace Med_App_API.Dto
{
    public class PatientsVaccinatesForCreation
    {
        public int PatientFKId { get; set; }
        public int VacinesFKId { get; set; }
        public DateTime TimeOfVaccination { get; set; }
        public double DosageMl { get; set; }
        public string? Description { get; set; }
    }
}