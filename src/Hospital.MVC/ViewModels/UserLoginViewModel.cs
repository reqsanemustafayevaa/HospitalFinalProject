using System.ComponentModel.DataAnnotations;

namespace Hospital.MVC.ViewModels
{
    public class UserLoginViewModel
    {
        [StringLength(maximumLength: 50)]
        public string UserName { get; set; }
        [StringLength(maximumLength: 20), MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        // [Required]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
