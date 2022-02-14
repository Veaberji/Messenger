using Messenger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Messenger.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly UserManager<User> _userManager;

        public MessageController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
    }
}
