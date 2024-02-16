using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Models
{
    public class Profession:BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
       
        public List<Doctor>? Doctors { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
