using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndGate.Server.Loopers;

namespace EndGate.Server
{
    internal class GameLoopCallbacks
    {
        public TimedCallback UpdateLoopCallback { get; set; }
        public TimedCallback PushLoopCallback { get; set; }
    }
}
