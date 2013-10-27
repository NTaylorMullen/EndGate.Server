using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EndGate.Server
{
    public interface IUpdateable
    {
        void Update(GameTime gameTime);
    }
}
