using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    class ActualData
    {
        public string text = "HELLO";
        public int instances = 0;
        public bool gameStart = false;
        public bool roundComplete = false;

        public bool[] isAlive = new bool[3];
        public bool[] takenShot = new bool[3];
    }

    class NPC
    {
        public int instance;

        public NPC(int instance)
        {
            this.instance = instance;
        }

        public int TakeShot()
        {
            Random random = new Random();
            int result = random.Next(1, 2);

            if (result == instance)
                result = 3;

            return result;
        }
    }


    class ClientData : MarshalByRefObject
    {
        public static ActualData data = new ActualData();

        public void ResetGame()
        {
            data.isAlive[0] = true;
            data.isAlive[1] = true;
            data.isAlive[2] = true;

            data.takenShot[0] = false;
            data.takenShot[1] = false;
            data.takenShot[2] = false;
        }

        public void Connect()
        {
            // Increment instances
            data.instances++;
            Console.WriteLine("Client " + data.instances + " has connected");
        }

        public void Disconnect()
        {
            // Decrement instances
            Console.WriteLine("Client has disconnected");
            data.instances--;
        }

        public void SayHello(string text)
        {
            Console.WriteLine("The Client says " + text);
        }

        public string GetText()
        {
            return data.text;
        }

        public int GetInstance()
        {
            return data.instances;
        }

        public void StartGame()
        {
            data.gameStart = true;
            Console.WriteLine("The Game Has started");

            ResetGame();
        }

        public bool GetGameStart()
        {
            return data.gameStart;
        }

        public bool GetRoundComplete()
        {
            return data.roundComplete;
        }

        public bool GetIsAlive(int instance)
        {
            return data.isAlive[instance - 1];
        }

        public void Shoot(int instance, int target)
        {
            data.takenShot[instance - 1] = true;
            data.isAlive[target - 1] = false;
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
