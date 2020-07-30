using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Med_App_API.Models
{
    public class Physician
    {
        public int Id { get; set; }
        public User User { get; set; }
        [ForeignKey("User")] public int UserFKId { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}