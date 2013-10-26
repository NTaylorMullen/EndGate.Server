using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace EndGate.Server
{
    public class UserManager<T> where T : IUser
    {
        private ConcurrentDictionary<string, T> _users;
        private long _userIds;

        public UserManager()
        {
            _users = new ConcurrentDictionary<string, T>();
            _userIds = 0;
        }

        public T this[string connectionId]
        {
            get { return _users[connectionId]; }
        }

        public T this[HubCallerContext context]
        {
            get { return _users[context.ConnectionId]; }
        }

        public ICollection<T> Users
        {
            get
            {
                return _users.Values;
            }
        }

        public T NewUser(string connectionId)
        {
            var user = default(T);

            user.ID = Interlocked.Increment(ref _userIds);
            user.ConnectionID = connectionId;

            _users.TryAdd(connectionId, user);

            return user;
        }
        public T NewUser(HubCallerContext context)
        {
            return NewUser(context.ConnectionId);
        }

        public T RemoveUser(string connectionId)
        {
            T user;
            _users.TryRemove(connectionId, out user);

            return user;
        }
        public T RemoveUser(HubCallerContext context)
        {
            return RemoveUser(context.ConnectionId);
        }
    }
}