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
        public static void SendWaitingMessage(int ConnectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ServerPackets.swelcomeMessage);
            buffer.WriteString("sei nella Wating room ora devi aspettare altri utenti");
            ClientManager.SendDataTo(ConnectionID, buffer.ToArray());
            buffer.Dispose();
        }
        public static void SendNumPlayer(int ConnectionID) // fa partire il gioco se ci sono i player 
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ServerPackets.swelcomeMessage);
            int N_player = ClientManager.client.Count;
            if(N_player<1 && N_player>6) // da cambiare in <=
                buffer.WriteString("non si puo giocare"); // si aspetta
            else
                buffer.WriteString("si puo giocare"); // si gioca
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
    }
}
