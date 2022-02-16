using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Messenger.Models
{
    [Authorize]
    public class MessengerHub : Hub
    {
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
    }
}
