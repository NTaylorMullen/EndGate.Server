using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndGate.Server.SignalR
{
    public interface IUserFactory<T>
    {
        T Create(long userId, string connectionId);
    }
}
