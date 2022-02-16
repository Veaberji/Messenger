using Messenger.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Messenger.Models
{
    [Authorize]
    public class MessengerHub : Hub
    {
        private readonly ConnectedUsersProvider _connectedUsersProvider;

        public MessengerHub(ConnectedUsersProvider connectedUsersProvider)
        {
            _connectedUsersProvider = connectedUsersProvider;
        }

        public async Task Send(Message message)
        {
            if (message.Sender != message.Receiver)
            {
                await Clients.User(message.Receiver)
                    .SendAsync("ReceiveMessage", message);
                await Clients.User(message.Receiver)
                    .SendAsync("Notify", message);
            }
            await Clients.Caller.SendAsync("ReceiveMessage", message);

        }

        public override async Task OnConnectedAsync()
        {
            var userName = GetUser();
            if (!string.IsNullOrWhiteSpace(userName))
            {
                _connectedUsersProvider.AddUser(userName);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userName = GetUser();
            if (!string.IsNullOrWhiteSpace(userName))
            {
                _connectedUsersProvider.RemoveUser(userName);
            }
            await base.OnDisconnectedAsync(exception);
        }

        private string GetUser()
        {
            return Context.User.Identity.Name;
        }
    }
}
