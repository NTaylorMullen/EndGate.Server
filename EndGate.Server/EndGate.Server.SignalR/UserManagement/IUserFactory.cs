using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndGate.Server.SignalR
{
    /// <summary>
    /// A user factory used to create users for the <see cref="UserManager"/>.
    /// </summary>
    /// <typeparam name="T">An <see cref="IUser"/> type to create.</typeparam>
    public interface IUserFactory<T>
    {
        /// <summary>
        /// Creates a user based off of the provided userId and connectionId.
        /// </summary>
        /// <param name="userId">A unique integer value to represent the user that is to be created.</param>
        /// <param name="connectionId">The SignalR connection id.</param>
        /// <returns>Returns the created user.</returns>
        T Create(long userId, string connectionId);
    }
}
