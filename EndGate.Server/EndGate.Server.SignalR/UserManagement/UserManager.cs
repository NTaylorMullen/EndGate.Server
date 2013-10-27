using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace EndGate.Server.SignalR
{
    public class UserManager<T> where T : IUser, new()
    {
        private ConcurrentDictionary<string, T> _users;
        private IUserFactory<T> _userFactory;
        private long _userIds;

        public UserManager()
            : this(new DefaultUserFactory<T>())
        {
        }

        public UserManager(IUserFactory<T> factory)
        {
            _users = new ConcurrentDictionary<string, T>();
            _userIds = 0;
            _userFactory = factory;
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

        public T AddUser(string connectionId)
        {
            var user = _userFactory.Create(Interlocked.Increment(ref _userIds), connectionId);

            _users.TryAdd(connectionId, user);

            return user;
        }
        public T AddUser(HubCallerContext context)
        {
            return AddUser(context.ConnectionId);
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