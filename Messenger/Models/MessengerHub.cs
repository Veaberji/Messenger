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

        //public async Task Send(string receiver, string theme, string message)
        //{
        //    var sender = await _userManager.GetUserAsync(Context.User);

        //    if (sender.UserName != receiver)
        //    {
        //        await Clients.User(receiver).SendAsync("ReceiveMessage", sender.UserName, theme, message);
        //    }
        //    await Clients.Caller.SendAsync("ConfirmMessage", receiver, theme, message);

        //}

        public async Task Send(Message message)
        {
            //var sender = await _userManager.GetUserAsync(Context.User);

            if (message.Sender != message.Receiver)
            {
                await Clients.User(message.Receiver)
                    .SendAsync("ReceiveMessage", message);
            }
            //await Clients.Caller.SendAsync("ConfirmMessage", message.Receiver, message.Theme, message.Body);
            await Clients.Caller.SendAsync("ConfirmMessage", message);

        }

        //public async Task Send(string receiver, string message)
        //{
        //    string fromUserId = Context.ConnectionId;

        //    var receiver = _userManager.Users.FirstOrDefault(x => x.Id == receiver);
        //    var fromUser = _userManager.Users.FirstOrDefault(x => x.Id == fromUserId);

        //    if (receiver != null && fromUser != null)
        //    {
        //        await Clients.Users(receiver).SendAsync("Receive", fromUserId, fromUser.UserName, message);
        //        await Clients.Caller.SendAsync("Receive", receiver, fromUser.UserName, message);

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
