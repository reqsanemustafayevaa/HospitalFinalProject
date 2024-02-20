using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Models
{
    public class Slider:BaseEntity
    {
        [Required]
        [StringLength(maximumLength:40)]
        public string Title {  get; set; }

        [Required]
        [StringLength(maximumLength: 200)]
        public string Description {  get; set; }
        [StringLength(maximumLength:100)]
        public string? ImageUrl {  get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
