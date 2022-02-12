using Microsoft.AspNetCore.Identity;
using System;

namespace Messenger.Models
{
    public class User : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
