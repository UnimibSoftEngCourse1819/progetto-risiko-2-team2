using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RiskServer1
{
    class ClientManager
    {
        public static Dictionary<int, Client> client = new Dictionary<int, Client>();


        public static void CreateNewConnection(TcpClient tempclient)
        {
            Client newclient = new Client();
            newclient.socket = tempclient;
            newclient.COnnectionID = ((IPEndPoint)tempclient.Client.RemoteEndPoint).Port;
            newclient.Start();
            client.Add(newclient.COnnectionID, newclient); // connessione al server

            DataSender.SendWelcomeMessage(newclient.COnnectionID);
        }
        public static void SendDataTo(int COnnectionID, byte[] data)// mando dati attraverso la network
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger(data.GetUpperBound(0) - data.GetLowerBound(0) + 1);
            buffer.writeBytes(data);
            client[COnnectionID].stream.BeginWrite(buffer.ToArray(), 0, buffer.Length(), null, null);
            buffer.Dispose();
        }
    }
}
