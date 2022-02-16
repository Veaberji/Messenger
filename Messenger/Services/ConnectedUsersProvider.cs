using System.Collections.Generic;

namespace Messenger.Services
{
    public class ConnectedUsersProvider
    {
        private readonly SortedSet<string> _connectedUsers;

        public ConnectedUsersProvider()
        {
            _connectedUsers = new SortedSet<string>();
        }

        public void AddUser(string userName)
        {
            _connectedUsers.Add(userName);
        }

        public void RemoveUser(string userName)
        {
            _connectedUsers.Remove(userName);
        }

        public IEnumerable<string> GetConnectedUsers()
        {
            return _connectedUsers;
        }
    }
}
