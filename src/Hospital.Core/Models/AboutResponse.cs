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
    public class AboutResponse:BaseEntity
    {
        [Required]
        [StringLength(60)]
        public string Response1 {  get; set; }
        [Required]
        [StringLength(60)]
        public string Response2 { get; set; }
        [Required]
        [StringLength(60)]
        public string Response3 { get; set; }
        [Required]
        [StringLength(300)]
        public string Description1 {  get; set; }
        [Required]
        [StringLength(300)]
        public string Description2 { get; set; }
        [Required]
        [StringLength(300)]
        public string Description3 {  get; set; }
        [StringLength(100)]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile {  get; set; }

    }
}
