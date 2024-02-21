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
    public class About : BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        [Required]
        [StringLength(30)]
        public string TitleSpan { get; set; }
        [Required]
        [StringLength(100)]
        public string MainDescription { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }
        [StringLength(15)]
        [Required]

        public string HealthSections { get; set; }
        [StringLength(15)]
        [Required]

        public string HappyPatients{ get; set; }
        [StringLength(15)]
        [Required]
        public string QualityDoctors {  get; set; }

        [StringLength(100)]
        public string? ImageUrl {  get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
