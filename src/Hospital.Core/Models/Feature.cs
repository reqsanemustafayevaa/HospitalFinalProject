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
    public class Feature:BaseEntity
    {
        [Required]
        [StringLength(60)]
        public string Title {  get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        public string Item1 {  get; set; }
        [Required]
        [StringLength(50)]
        public string Item2 { get; set; }
        [Required]
        [StringLength(50)]
        public string Item3 {  get; set; }
        [Required]
        [StringLength(50)]
        public string Item4 { get; set; }
        [Required]
        [StringLength(50)]
        public string Item5 {  get; set; }
        [StringLength(100)]
        public string? ImageUrl {  get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
