﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndGate.Server
{
    public class GameRegistration
    {
        public Action<int> UpdateRateSetter { get; set; }
        public Action<int> PushRateSetter { get; set; }
    }
}
