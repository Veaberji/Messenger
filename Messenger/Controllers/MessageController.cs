using Messenger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly MessagesDbContext _messagesDbContext;

        public MessageController(UserManager<User> userManager, MessagesDbContext messagesDbContext)
        {
            _userManager = userManager;
            _messagesDbContext = messagesDbContext;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Message message)
        {
            Console.WriteLine(message);
            await _messagesDbContext.Messages.AddAsync(message);
            var result = await _messagesDbContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        public IActionResult History()
        {
            var user = GetCurrentUser();
            var messages = _messagesDbContext.Messages
                .Where(m => m.Sender == user || m.Receiver == user).ToList();

            return View(messages);
        }

        public IActionResult IncomingMessages()
        {
            var user = GetCurrentUser();
            var messages = _messagesDbContext.Messages
                .Where(m => m.Receiver == user).ToList();

            return View(messages);
        }

        private string GetCurrentUser()
        {
            return HttpContext.User.Identity?.Name ?? "";
        }
    }
}
