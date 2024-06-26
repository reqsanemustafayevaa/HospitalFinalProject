﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Models
{
    public class Doctor : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 7)]
        public string Fullname { get; set; }

        [StringLength(maximumLength: 300, MinimumLength = 20)]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 10)]
        public string Phone { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]

        public string Office { get; set; }
        [Required]
        [StringLength(maximumLength: 60, MinimumLength = 16)]
        public string Email { get; set; }

        [StringLength(maximumLength:10,MinimumLength =4)]
        public string Year { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 4)]
        public string Degree { get; set; }
        [StringLength(maximumLength: 70, MinimumLength = 2)]
        public string Institute { get; set; }

        [StringLength(maximumLength: 100)]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public Profession? Profession { get; set; }
        public int ProfessionId { get; set; }

     
        public List<Appointment>? Appointments { get; set; }

        public List<WorkSchedule>? WorkSchedules { get; set; }

        

     
























    }
}
