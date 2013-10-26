using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EndGate.Server
{
    public interface IUser
    {
        long ID { get; set; }
        [JsonIgnore]
        string ConnectionID { get; set; }
    }
}
