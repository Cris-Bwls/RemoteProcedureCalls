using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
	class ClientData : MarshalByRefObject
	{
		public void Connect(int port)
		{
			//Get a reference to the player on the server
			string clientDataURL = "tcp://localhost:" + port + "/" + "ServerData";
			ServerData serverData = (ServerData)Activator.GetObject(typeof(ServerData), clientDataURL);
			serverData.Connect();
		}

		public void SayHello(string text)
		{
			Console.WriteLine("The Client says " + text);
		}
	}

	class ServerData : MarshalByRefObject
	{
		public void Connect()
		{
			Console.WriteLine("Connected");
		}
	}
}
