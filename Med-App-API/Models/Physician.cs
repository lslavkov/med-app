using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace Med_App_API.Models
{
    public class Physician
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("User")] public int UserFKId { get; set; }
        public string FullName { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
        // public bool IsMonday { get; set; }
        // public TimeSpan? MondayStart { get; set; }
        // public TimeSpan? MondayFinish{ get; set; }
        // public bool IsTuesday{ get; set; }
        // public TimeSpan? TuesdayStart { get; set; }
        // public TimeSpan? TuesdayFinish{ get; set; }
        // public bool IsWednesday { get; set; }
        // public TimeSpan? WednesdayStart { get; set; }
        // public TimeSpan? WednesdayFinish{ get; set; }
        // public bool IsThursday { get; set; }
        // public TimeSpan? ThursdayStart { get; set; }
        // public TimeSpan? ThursdayFinish{ get; set; }
        // public bool IsFriday { get; set; }
        // public TimeSpan? FridayStart { get; set; }
        // public TimeSpan? FridayFinish{ get; set; }
     
    }
}