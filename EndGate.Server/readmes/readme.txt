EndGate.Multiplayer
-------------------
EndGate Multiplayer is a combination of server and client frameworks to provide an easy
API to build HTML5 Multiplayer games.  EndGate Multiplayer utilizes SignalR to communicate
between the server and Client.  EndGate is used to write the game client.  Lastly the
EndGate.Server is used to supplement SignalR in providing a consistent experience from
the server to the client.

To learn SignalR visit: http://go.microsoft.com/fwlink/?LinkId=272764
To learn EndGate visit: http://endgate.net

Mapping the Hubs connection
----------------------------
To enable SignalR in your application, create a class called Startup with the following:

using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyApplication.Startup))]

namespace MyApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}