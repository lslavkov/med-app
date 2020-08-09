using System;

namespace Med_App_API.Dto
{
    public class AppointmentPhysicianForListDto
    {
        public int Id { get; set; }
        public int PatientFKId { get; set; }
        public string PatientFullName { get; set; }
        public DateTime StartOfAppointment { get; set; }
        public DateTime EndOfAppointment { get; set; }
        public string? Description { get; set; }
        public string TypeOfAppointment { get; set; }
    }
}