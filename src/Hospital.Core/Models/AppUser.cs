using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Models
{
    public class AppUser:IdentityUser
    {
        [StringLength(maximumLength: 100,MinimumLength =6)]
        public string FullName { get; set; }


      
        public List<Appointment>? Appointments { get; set; }

       
    }
}
