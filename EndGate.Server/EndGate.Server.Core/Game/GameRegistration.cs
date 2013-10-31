using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndGate.Server
{
    /// <summary>
    /// An object that holds important game registration information.
    /// </summary>
    public class GameRegistration
    {
        /// <summary>
        /// Used to set the underlying update rate of the game.
        /// </summary>
        public Action<int> UpdateRateSetter { get; set; }
        /// <summary>
        /// Used to set the underlying push rate of the game.
        /// </summary>
        public Action<int> PushRateSetter { get; set; }
    }
}
