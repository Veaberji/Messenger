using Microsoft.AspNetCore.SignalR;

namespace Messenger.Models
{
    public class UserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection?.User.Identity.Name;
        }
    }
}
