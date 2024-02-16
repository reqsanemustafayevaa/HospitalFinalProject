using System.ComponentModel.DataAnnotations;

namespace Hospital.MVC.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [StringLength(maximumLength:30,MinimumLength =4)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 45, MinimumLength = 6)]
        public string FullName {  get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 13)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength:30, MinimumLength = 8)]
        [Compare(nameof(ConfirmPassword))]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]

        [StringLength(maximumLength: 30, MinimumLength = 8)]

        public string ConfirmPassword {  get; set; }

    }
}
