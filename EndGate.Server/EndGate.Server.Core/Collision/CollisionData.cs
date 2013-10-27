using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndGate.Server;

namespace EndGate.Server.Collision
{
    public class CollisionData
    {
        public CollisionData(Collidable with)
        {
            With = with;
        }

        public Collidable With { get; private set; }
    }
}
