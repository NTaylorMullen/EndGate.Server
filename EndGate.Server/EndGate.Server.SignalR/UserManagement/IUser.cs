using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EndGate.Server
{
    /// <summary>
    /// Interface used to represent a user object that can be added to a <see cref="UserManager"/>.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Unique ID used to represent the user.
        /// </summary>
        long ID { get; set; }

        /// <summary>
        /// SignalR connection id used to address a user.
        /// </summary>
        [JsonIgnore]
        string ConnectionID { get; set; }
    }
}
