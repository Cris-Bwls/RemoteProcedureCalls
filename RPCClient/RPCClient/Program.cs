using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;

namespace RPCClient
{
	class Program
	{
		static int port = 8080;

		static void Main(string[] args)
		{
			//Create Client
			TcpClientChannel channel = new TcpClientChannel();
			ChannelServices.RegisterChannel(channel, false);

			RemotingConfiguration.RegisterWellKnownServiceType(typeof(ServerData), "ServerData", WellKnownObjectMode.SingleCall);

			//Get a reference to the player on the server
			string clientDataURL = "tcp://localhost:" + port + "/" + "ClientData";
			ClientData clientData = (ClientData)Activator.GetObject(typeof(ClientData), clientDataURL);
			//clientData.Connect(port);

			while (true)
			{
				Console.Write("Type a message to the server or type 'quit' to exit\n");
				string text = Console.ReadLine();
				if (text == "quit")
					break;
				//RPC: Call function on server
				clientData.SayHello(text);
			}
		}
	}
}
