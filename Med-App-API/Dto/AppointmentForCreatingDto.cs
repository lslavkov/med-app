using System;

namespace Med_App_API.Dto
{
    public class AppointmentForCreatingDto
    {
        public int PatientFKId { get; set; }
        public string? PatientFullName { get; set; }
        public int PhysicianFKId { get; set; }
        public string? PhysicianFullName { get; set; }
        public DateTime StartOfAppointment { get; set; }
        public string? Description { get; set; }
        public DateTime EndOfAppointment { get; set; }

        public AppointmentForCreatingDto()
        {
            EndOfAppointment = StartOfAppointment.AddMinutes(15);
        }
    }
}