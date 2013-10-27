using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EndGate.Server.SignalR
{
    public class DefaultUserFactory<T> : IUserFactory<T> where T : IUser, new()
    {
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
