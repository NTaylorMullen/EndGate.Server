using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EndGate.Server;

namespace EndGate.Server
{
    /// <summary>
    /// Defines a game time class that is used to manage update timing execution as well as total game time.
    /// </summary>
    public class GameTime
    {
        private readonly DateTime _start;

        /// <summary>
        /// Creates a new instance of the GameTime object.
        /// </summary>
        public GameTime()
        {
            _start = DateTime.UtcNow;
            Now = _start;
        }

        /// <summary>
        /// Gets the elapsed time since the last update.
        /// </summary>
        public TimeSpan Elapsed { get; private set; }
        /// <summary>
        /// Gets the total amount of time surpassed since construction.
        /// </summary>
        public TimeSpan Total { get; private set; }
        /// <summary>
        /// Gets the current date time at the start of the update.
        /// </summary>
        public DateTime Now { get; private set; }
        /// <summary>
        /// Updates the game time object.  Causes the gameTime to refresh all its components.
        /// </summary>
        public void Update()
        {
            var now = DateTime.UtcNow;

            Elapsed = now - Now;
            Total = now - _start;
            Now = now;
        }
    }
}
