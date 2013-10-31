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
    /// <summary>
    /// A user manager that allows management of users.  This class maps SignalR connection ids to generated users.
    /// </summary>
    /// <typeparam name="T">The type of user to create.</typeparam>
    public class UserManager<T> where T : IUser, new()
    {
        private ConcurrentDictionary<string, T> _users;
        private IUserFactory<T> _userFactory;
        private long _userIds;

        /// <summary>
        /// Initiates a new UserManager with a <see cref="DefaultUserFactory"/>.
        /// </summary>
        public UserManager()
            : this(new DefaultUserFactory<T>())
        {
        }

        /// <summary>
        /// Initiates a new UserManager with a user provided <see cref="IUserFactory"/>.
        /// </summary>
        /// <param name="factory"></param>
        public UserManager(IUserFactory<T> factory)
        {
            _users = new ConcurrentDictionary<string, T>();
            _userIds = 0;
            _userFactory = factory;
        }

        /// <summary>
        /// Retrieves a user based on the provided connection id.
        /// </summary>
        /// <param name="connectionId">The SignalR connection id.</param>
        /// <returns>The user assigned to the provided connection id.</returns>
        public T this[string connectionId]
        {
            get { return _users[connectionId]; }
        }

        /// <summary>
        /// Retrieves a user based on the provided hub caller context's connection id.
        /// </summary>
        /// <param name="context">The SignalR hub caller context.</param>
        /// <returns>The user assigned to the provided hub caller context's connection id.</returns>
        public T this[HubCallerContext context]
        {
            get { return _users[context.ConnectionId]; }
        }

        /// <summary>
        /// Retrieves a user who's user id matches the provided user id.
        /// </summary>
        /// <param name="userId">The unique user id assigned to a user.</param>
        /// <returns>The user with a matching user id.</returns>
        public T this[long userId]
        {
            get { return _users.Values.First(user => user.ID == userId); }
        }

        /// <summary>
        /// List of users that have been created by the UserManager.
        /// </summary>
        public ICollection<T> Users
        {
            get
            {
                return _users.Values;
            }
        }

        /// <summary>
        /// Adds a new user to the UserManager's list of Users that is mapped to the provided connection id.
        /// </summary>
        /// <param name="connectionId">The SignalR connection id.</param>
        /// <returns>The created user which is matched to the connection id.</returns>
        public T AddUser(string connectionId)
        {
            var user = _userFactory.Create(Interlocked.Increment(ref _userIds), connectionId);

            _users.TryAdd(connectionId, user);

            return user;
        }

        /// <summary>
        /// Adds a new user to the UserManager's list of Users that is mapped to the provided hub caller context's connection id.
        /// </summary>
        /// <param name="context">The SignalR clients hub caller context.</param>
        /// <returns>The created user which is matched to the hub caller context's connection id.</returns>
        public T AddUser(HubCallerContext context)
        {
            return AddUser(context.ConnectionId);
        }

        /// <summary>
        /// Removes the user associated with the provided connection id.
        /// </summary>
        /// <param name="connectionId">The SignalR connection id.</param>
        /// <returns>Returns the user associated with the connection id.</returns>
        public T RemoveUser(string connectionId)
        {
            T user;

            _users.TryRemove(connectionId, out user);

            return user;
        }

        /// <summary>
        /// Removes the user associated with the provided hub caller context's connection id.
        /// </summary>
        /// <param name="context">The SignalR clients hub caller context.</param>
        /// <returns>Returns the user associated with the hub caller context's connection id.</returns>
        public T RemoveUser(HubCallerContext context)
        {
            return RemoveUser(context.ConnectionId);
        }
    }
}