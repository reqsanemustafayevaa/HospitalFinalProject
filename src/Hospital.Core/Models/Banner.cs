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
    public class Banner:BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string Title {  get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength:16)]
        public string Phone {  get; set; }
        [StringLength(maximumLength:100)]
        public string? ImageUrl {  get; set; }
        [NotMapped]
        public IFormFile? ImageFile {  get; set; }
    }
}
