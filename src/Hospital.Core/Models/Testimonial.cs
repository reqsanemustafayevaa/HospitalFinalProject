using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Models
{
    public class Testimonial:BaseEntity
    {
        [StringLength(35)]
        [Required]
        public string FullName {  get; set; }
        [StringLength(150)]
        [Required]
        public string Comment { get; set; }
        [StringLength(20)]
        [Required]
        public string Position {  get; set; }
        [StringLength(100)]
        public string? ImageUrl {  get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
