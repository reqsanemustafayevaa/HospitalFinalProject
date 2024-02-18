using Hospital.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Models
{
    public class Appointment:BaseEntity
    {
        [Required]

        public DateTime AppointmentDate { get; set; }
        [Required]

        public TimeSpan AppointmentStartTime { get; set; }
        public TimeSpan AppointmentEndTime { get; set; }
        public Doctor? Doctor { get; set; }
        public int DoctorId { get; set; }
        public Profession? Profession { get; set; }
        public int ProfessionId { get; set; }

        [StringLength(35)]
        [Required]
        public string FullName { get; set; }
        [Required]
        [StringLength(maximumLength: 20)]
        [DataType(DataType.PhoneNumber)]

        public string Phone { get; set; }
        [Required]
        [StringLength(maximumLength: 60, MinimumLength = 15)]

        public string Email { get; set; }
        [StringLength(100)]

        public string Note { get; set; }


        public string? AppUserId { get; set; }
        public AppUser? appUser { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; }

        [StringLength(maximumLength:60)]
        public string? Comment {  get; set; }


    }
}
