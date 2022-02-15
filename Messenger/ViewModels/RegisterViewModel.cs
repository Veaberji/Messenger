using Messenger.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Messenger.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(IdentityConstrains.MaxStringLength)]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(IdentityConstrains.MaxStringLength)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(IdentityConstrains.MaxStringLength)]
        public string Email { get; set; }
    }
}
