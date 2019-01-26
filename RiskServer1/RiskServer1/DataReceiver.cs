using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskServer1
{
    public enum ClientPackets
    {
        cHelloServer = 1,
    }

    class DataReceiver
    {
        public static void HandleHelloServer(int connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.writeBytes(data);
            int packetID = buffer.ReadInteger();
            string msg = buffer.ReadString();
            buffer.Dispose();
           
            switch(msg)
            {
                case "ora sono connesso al server":
                    {
                        Console.WriteLine(msg);
                        DataSender.SendWaitingMessage(connectionID);
                        break;
                    }
                case "quanti player sono connessi ?":
                    {
                        Console.WriteLine(msg);
                        DataSender.SendNumPlayer(connectionID);
                        break;
                    }
                case "start game": // mando un broadcast ai player
                    {
                        Console.WriteLine(msg);
                        foreach (KeyValuePair<int,Client > keyValue in ClientManager.client)
                        {
                            Console.WriteLine("starting new player ...");
                            DataSender.SendStartGame(keyValue.Key);
                        }
                        break;
                    }

            }
           
        }
    }
}
