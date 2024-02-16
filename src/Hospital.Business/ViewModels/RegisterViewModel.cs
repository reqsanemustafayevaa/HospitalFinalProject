using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.ViewModels
{
    public class RegisterViewModel
    {
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [Required]
        public string UserName { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [Required]
        public string Fullname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(maximumLength: 30, MinimumLength = 8)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
