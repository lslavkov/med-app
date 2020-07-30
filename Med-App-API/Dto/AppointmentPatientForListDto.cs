using System;

namespace Med_App_API.Dto
{
    public class AppointmentPatientForListDto
    {
        public int Id { get; set; }
        public int PhysicianFKId { get; set; }
        public string PhysicianFullName { get; set; }
        public DateTime StartOfAppointment { get; set; }
        public DateTime EndOfAppointment { get; set; }
        public string? Description { get; set; }
    }
}