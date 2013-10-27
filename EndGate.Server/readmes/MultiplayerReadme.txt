EndGate.Multiplayer
-------------------
EndGate Multiplayer is a combination of server and client frameworks to provide an easy
API to build HTML5 Multiplayer games.  EndGate Multiplayer utilizes SignalR to communicate
between the server and Client.  EndGate is used to write the game client in TypeScript.  
Lastly the EndGate.Server is used to supplement SignalR in providing a consistent experience 
from the server to the client.

To learn EndGate visit: http://endgate.net
To learn SignalR visit: http://go.microsoft.com/fwlink/?LinkId=272764

To download TypeScript visit: http://www.typescriptlang.org/


Enabling the client: Adding the scripts
---------------------------------------
To enable a client that is server connected you must include the following scripts (in the order 
shown). Ensure that you replace the Xs with the appropriate version numbers:

<script src="Scripts/jquery-X.X.X.js"></script>
<script src="Scripts/jquery.signalR-X.X.X.js"></script>

<!-- Add this script if you'd like to use Hubs in your game
	<script src="signalr/hubs"></script>
-->

<script src="Scripts/endgate-X.X.X.js"></script>


Enabling the server: Mapping the Hubs connection
------------------------------------------------
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