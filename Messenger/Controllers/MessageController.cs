using Messenger.Models;
using Messenger.Services;
using Messenger.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly MessagesDbContext _messagesDbContext;
        private readonly ConnectedUsersProvider _connectedUsersProvider;


        public MessageController(UserManager<User> userManager, MessagesDbContext messagesDbContext, ConnectedUsersProvider connectedUsersProvider)
        {
            _userManager = userManager;
            _messagesDbContext = messagesDbContext;
            _connectedUsersProvider = connectedUsersProvider;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            var connectedUsers = _connectedUsersProvider.GetConnectedUsers();
            var usersConnectedStatusViewModel = users.
                Select(user => user.UserName).
                Select(userName => new UsersConnectedStatusViewModel
                { UserName = userName, IsConnected = connectedUsers.Contains(userName) }).ToList();
            return View(usersConnectedStatusViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Message message)
        {
            if (!await IsCorrectUsers(message))
            {
                return BadRequest("User(s) not Found");
            }

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

        private async Task<bool> IsUserExists(string userName)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);
            return user != null;
        }

        private async Task<bool> IsCorrectUsers(Message message)
        {
            return await IsUserExists(message.Sender) && await IsUserExists(message.Receiver);
        }
    }
}
