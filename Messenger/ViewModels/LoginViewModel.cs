using Messenger.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Messenger.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(IdentityConstrains.MaxStringLength)]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(IdentityConstrains.MaxStringLength)]
        public string Password { get; set; }
    }
}
