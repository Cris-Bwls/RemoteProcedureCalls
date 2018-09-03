using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;

namespace RPCServer
{
	class Program
	{
		static private int port = 8080;

		static void Main(string[] args)
		{
			//Create Server
			TcpServerChannel channel = new TcpServerChannel(port);
			ChannelServices.RegisterChannel(channel, false);
			
			//Register Player
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(ClientData), "ClientData", WellKnownObjectMode.SingleCall);
			

			ChannelDataStore client1;
			Console.WriteLine("Listening for requests");
			Console.WriteLine("Press 'enter' to exit...");
			if (Console.ReadKey().Key == ConsoleKey.F)
			{
				client1 = (ChannelDataStore)channel.ChannelData;
				var uriArray = client1.ChannelUris;
				foreach (string uri in uriArray)
				{
					var urlArray = channel.GetUrlsForUri(uri);
					foreach (string url in urlArray)
					{
						ClientData proxyClient = new ClientData();
						proxyClient = (ClientData)RemotingServices.Connect(proxyClient.GetType(), url);
					}
				}
			}			
			Console.ReadLine();
		}
	}
}
