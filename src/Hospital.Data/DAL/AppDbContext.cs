﻿using Hospital.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Data.DAL
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) 
        {
            
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutResponse> AboutsResponse { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Testimonial> Testimonials { get; set;}
        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<Banner> Banners { get; set; }



    }
    
}
