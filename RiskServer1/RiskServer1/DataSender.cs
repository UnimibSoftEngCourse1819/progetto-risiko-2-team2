using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskServer1
{
    public enum ServerPackets
    {
        swelcomeMessage = 1,
    }
    class DataSender
    {
        public static void SendWelcomeMessage(int COnnectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ServerPackets.swelcomeMessage);
            buffer.WriteString("benvenuto nel server mettiti comodo");
            ClientManager.SendDataTo(COnnectionID, buffer.ToArray());
            buffer.Dispose();
        }
        /*     public static void SendWaitingMessage(int ConnectionID)
             {
                 ByteBuffer buffer = new ByteBuffer();
                 buffer.WriteInteger((int)ServerPackets.swelcomeMessage);
                 buffer.WriteString("sei nella Wating room ora devi aspettare altri utenti");
                 ClientManager.SendDataTo(ConnectionID, buffer.ToArray());
                 buffer.Dispose();
             }
       */
        public static void SendAskName(int ConnectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ServerPackets.swelcomeMessage);
            buffer.WriteString("Come ti chiami ?");
            ClientManager.SendDataTo(ConnectionID, buffer.ToArray());
            buffer.Dispose();
        }
        public static void SendOk(int ConnectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ServerPackets.swelcomeMessage);
            buffer.WriteString("ok");
            ClientManager.SendDataTo(ConnectionID, buffer.ToArray());
            buffer.Dispose();
        }
        public static void SendNumPlayer(int ConnectionID) //  
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ServerPackets.swelcomeMessage);
            int N_player = ClientManager.client.Count;
            //Console.WriteLine(N_player);
            buffer.WriteString(N_player.ToString()); //           
            ClientManager.SendDataTo(ConnectionID, buffer.ToArray());
            buffer.Dispose();
        }

        public static void SendStartGame(int ConnectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ServerPackets.swelcomeMessage);
            buffer.WriteString("startgame");
            ClientManager.SendDataTo(ConnectionID, buffer.ToArray());
            buffer.Dispose();
        }
        public static void SendNewPlayer(int ConnectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ServerPackets.swelcomeMessage);
            buffer.WriteString("NewPlayer");
            ClientManager.SendDataTo(ConnectionID, buffer.ToArray());
            buffer.Dispose();
        }
        public static void SendPlayerQuit(int ConnectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ServerPackets.swelcomeMessage);
            buffer.WriteString("PlayerQuit");
            ClientManager.SendDataTo(ConnectionID, buffer.ToArray());
            buffer.Dispose();
        }
        public static void SendTurn(int ConnectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ServerPackets.swelcomeMessage);
            buffer.WriteString("Your Go");
            ClientManager.SendDataTo(ConnectionID, buffer.ToArray());
            buffer.Dispose();
        }
    }
}
