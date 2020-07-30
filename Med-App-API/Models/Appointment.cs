#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Med_App_API.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Physician Physician { get; set; }
        [ForeignKey("Patient")] public int PatientFKId { get; set; }
        public string? PatientFullName { get; set; }
        [ForeignKey("Physician")] public int PhysicianFKId { get; set; }
        public string? PhysicianFullName { get; set; }
        public DateTime StartOfAppointment { get; set; }
        public DateTime EndOfAppointment { get; set; }
        public string? Description { get; set; }
    }
}