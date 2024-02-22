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
    public class Service:BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string Name {  get; set; }
        [Required]
        [StringLength(30)]
        public string Title {  get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
       
        [StringLength(100)]
        public string? ImageUrl {  get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
