using System;

namespace Med_App_API.Models
{
    public class Workday
    {
        public int Id { get; set; }
        public int PhysicianId { get; set; }
        public Physician Physician { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }
}