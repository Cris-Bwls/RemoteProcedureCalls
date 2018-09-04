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
		static private int port = 8080;

        static ClientData clientData;

        
		static void Main(string[] args)
		{
			//Create Client
			TcpClientChannel channel = new TcpClientChannel();
			ChannelServices.RegisterChannel(channel, false);

			//RemotingConfiguration.RegisterWellKnownServiceType(typeof(ServerData), "ServerData", WellKnownObjectMode.SingleCall);

			//Get a reference to the player on the server
			string clientDataURL = "tcp://localhost:" + port + "/" + "ClientData";
			clientData = (ClientData)Activator.GetObject(typeof(ClientData), clientDataURL);
            clientData.Connect();

            int instance = clientData.GetInstance();
            if (instance > 3)
            {
                Console.WriteLine("Maximum Players Reached,\n You have been disconnected");

                clientData.Disconnect();
                ChannelServices.UnregisterChannel(channel);
                Console.ReadLine();
                return;
            }
            Console.WriteLine("You are Player " + clientData.GetInstance());

			while (true)
			{
                Console.WriteLine("Welcome to the mexican standoff");
                Console.Write("Type 'start' to start the game or type 'quit' to exit\n");
				string text = Console.ReadLine();

                if (text == "start")
                    clientData.StartGame();

				if (text == "quit")       
                	break;

                bool gameStarted = clientData.GetGameStart();

                if (gameStarted)
                {
                    Console.WriteLine("The Game Has Started");
                    Console.WriteLine("You are player " + instance);

                    bool noTarget = true;
                    while (noTarget)
                    {
                        Console.WriteLine("Which Player do you shoot");
                        var target = Console.ReadKey(true).Key;
                        if (target == ConsoleKey.D0 + instance)
                        {
                            Console.WriteLine("You cant shoot yourself");
                        }
                        else
                        {
                            int targetNum = target - ConsoleKey.D0;
                            Console.WriteLine("You shoot Player" + targetNum);
                            clientData.Shoot(instance, targetNum);
                            noTarget = false;
                        }
                    }

                    bool roundOngoing = true;
                    while (roundOngoing)
                    {
                        if (clientData.GetRoundComplete())
                        {
                            roundOngoing = false;

                            if (!clientData.GetIsAlive(instance))
                                Console.WriteLine("You Died!");
                            Console.WriteLine("The Following Players survived!");

                            if (clientData.GetIsAlive(1))
                                Console.WriteLine("Player 1!");
                            if (clientData.GetIsAlive(2))
                                Console.WriteLine("Player 2!");
                            if (clientData.GetIsAlive(3))
                                Console.WriteLine("Player 3!");

                            Console.WriteLine("\n Press enter to continue");
                            Console.ReadLine();
                        }
                    }
                }
			}

            // Close Program
            clientData.Disconnect();
		}
	}
}
