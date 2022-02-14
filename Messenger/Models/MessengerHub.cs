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

        public async Task Send(string toUserId, string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);

            await Clients.User(toUserId).SendAsync("ReceiveMessage", user.UserName, message);
            await Clients.Caller.SendAsync("ConfirmMessage", toUserId, message);



            //await Clients.Caller.SendAsync("ReceiveMessage", user.UserName, message);

        }


        //public async Task Send(string toUserId, string message)
        //{
        //    string fromUserId = Context.ConnectionId;

        //    var toUser = _userManager.Users.FirstOrDefault(x => x.Id == toUserId);
        //    var fromUser = _userManager.Users.FirstOrDefault(x => x.Id == fromUserId);

        //    if (toUser != null && fromUser != null)
        //    {
        //        await Clients.Users(toUserId).SendAsync("Receive", fromUserId, fromUser.UserName, message);
        //        await Clients.Caller.SendAsync("Receive", toUserId, fromUser.UserName, message);

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
