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

            List<NPC> npcList = new List<NPC>();
			
			//Register Player
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(ClientData), "ClientData", WellKnownObjectMode.SingleCall);

            // GameLoop
            ActualData gameData = ClientData.data;
            
            bool gameRunning = true;
            while(gameRunning)
            {
                while (gameData.gameStart)
                {
                    if (gameData.instances < 3)
                    {
                        for (int i = 0; i < (3 - gameData.instances); ++i)
                        {
                            npcList.Add(new NPC(gameData.instances + 1 + i));
                        }

                    }

					if (gameData.takenShot[0] && gameData.takenShot[1] && gameData.takenShot[2])
					{
						gameData.roundComplete = true;
						gameData.gameStart = false;
						Console.WriteLine("The Game has finished");
					}
                }
            }
            // Press Escape to end Game Loop
            if (Console.ReadKey().Key == ConsoleKey.Escape)
                gameRunning = false;

			Console.WriteLine("Listening for requests");
			Console.WriteLine("Press 'enter' to exit...");

            ClientData.data.text = "Blah";

            Console.ReadLine();
		}
	}
}
