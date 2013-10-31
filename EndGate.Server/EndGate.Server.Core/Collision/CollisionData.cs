using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndGate.Server;

namespace EndGate.Server.Collision
{
    /// <summary>
    /// Defines a data object that is used to describe a collision event.
    /// </summary>
    public class CollisionData
    {
        /// <summary>
        /// Creates a new instance of the CollisionData object.
        /// </summary>
        /// <param name="with">Initial value of the With component of CollisionData.</param>
        public CollisionData(Collidable with)
        {
            With = with;
        }

        /// <summary>
        /// Who collided with you.
        /// </summary>
        public Collidable With { get; private set; }
    }
}
