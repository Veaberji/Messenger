using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Messenger.Models
{
    [Authorize]
    public class MessengerHub : Hub
    {
        private readonly UserManager<User> _userManager;

        public MessengerHub(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Send(string toUser, string theme, string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);

            if (user.UserName != toUser)
            {
                await Clients.User(toUser).SendAsync("ReceiveMessage", user.UserName, theme, message);
            }
            await Clients.Caller.SendAsync("ConfirmMessage", toUser, theme, message);



            //await Clients.Caller.SendAsync("ReceiveMessage", user.UserName, message);

        }


        //public async Task Send(string toUser, string message)
        //{
        //    string fromUserId = Context.ConnectionId;

        //    var toUser = _userManager.Users.FirstOrDefault(x => x.Id == toUser);
        //    var fromUser = _userManager.Users.FirstOrDefault(x => x.Id == fromUserId);

        //    if (toUser != null && fromUser != null)
        //    {
        //        await Clients.Users(toUser).SendAsync("Receive", fromUserId, fromUser.UserName, message);
        //        await Clients.Caller.SendAsync("Receive", toUser, fromUser.UserName, message);

        //    }
        //}

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} вошел в чат");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
