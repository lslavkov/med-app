using System;

namespace Med_App_API.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int PhysicianId { get; set; }
        public Physician Physician { get; set; }
        public DateTime TimeofAppointment { get; set; }
        public string TypeOfAppointment { get; set; }
    }
}