using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EndGate.Server
{
    /// <summary>
    /// Represents an object that can be Updated.
    /// </summary>
    public interface IUpdateable
    {
        /// <summary>
        /// Should be called frequently to perform time based operations.
        /// </summary>
        /// <param name="gameTime">The <see cref="Game"/>'s game time.</param>
        void Update(GameTime gameTime);
    }
}
