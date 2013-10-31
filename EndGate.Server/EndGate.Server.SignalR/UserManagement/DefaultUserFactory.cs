using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EndGate.Server.SignalR
{
    /// <summary>
    /// The default user factory used to create users for the <see cref="UserManager"/>.
    /// </summary>
    /// <typeparam name="T">An <see cref="IUser"/> type to create.</typeparam>
    public class DefaultUserFactory<T> : IUserFactory<T> where T : IUser, new()
    {
        /// <summary>
        /// Creates a user and sets the user's ID and ConnectionId to the provided userId and connectionId.
        /// </summary>
        /// <param name="userId">A unique integer value to represent the user that is to be created.</param>
        /// <param name="connectionId">The SignalR connection id.</param>
        /// <returns>Returns the created user.</returns>
        public T Create(long userId, string connectionId)
        {
            return new T
            {
                ID = userId,
                ConnectionID = connectionId
            };
        }
    }
}
