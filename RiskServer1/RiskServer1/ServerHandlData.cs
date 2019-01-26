using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskServer1
{
   
        static class ServerHandlData
        {
            public delegate void Packet(int connectionID, byte[] data); // legge tutti i dati sulla network
            public static Dictionary<int, Packet> packets = new Dictionary<int, Packet>();

            public static void InizializzaPacchetti()
            {
                packets.Add((int)ClientPackets.cHelloServer, DataReceiver.HandleHelloServer);
            }

            public static void HandleData(int connectionID, byte[] data)// gestisce la perdita di pacchetti
            {
                byte[] buffer = (byte[])data.Clone();
                int plength = 0;

                if (ClientManager.client[connectionID].buffer == null)
                    ClientManager.client[connectionID].buffer = new ByteBuffer();

                ClientManager.client[connectionID].buffer.writeBytes(buffer);
                if (ClientManager.client[connectionID].buffer.Count() == 0) // il messaggio era vuoto
                {
                    ClientManager.client[connectionID].buffer.Clear();
                    return;
                }
                if (ClientManager.client[connectionID].buffer.Length() >= 4) // il messaggio non era un integer
                {
                    plength = ClientManager.client[connectionID].buffer.ReadInteger(false);
                    if (plength <= 0)
                    {
                        ClientManager.client[connectionID].buffer.Clear();
                        return;
                    }
                }
                while (plength >= 0 && plength <= ClientManager.client[connectionID].buffer.Length() - 4) // usiamo gli int come packetidentifier 
                {
                    if (plength <= ClientManager.client[connectionID].buffer.Length() - 4)
                    {
                        ClientManager.client[connectionID].buffer.ReadInteger();
                        data = ClientManager.client[connectionID].buffer.ReadBytes(plength);
                        HandleDataPackets(connectionID, data);
                    }
                    plength = 0;
                    if (ClientManager.client[connectionID].buffer.Length() >= 4)
                    {
                        plength = ClientManager.client[connectionID].buffer.ReadInteger(false);
                        if (plength <= 0)
                        {
                            ClientManager.client[connectionID].buffer.Clear();
                            return;
                        }
                    }
                }
                if (plength <= 1)
                {
                    ClientManager.client[connectionID].buffer.Clear();
                }
            }

            private static void HandleDataPackets(int connectionID, byte[] data)
            {
                ByteBuffer buffer = new ByteBuffer();
                buffer.writeBytes(data);
                int packetID = buffer.ReadInteger();
                buffer.Dispose();
                if (packets.TryGetValue(packetID, out Packet packet))
                {
                    packet.Invoke(connectionID, data);
                }
            }
        }
}
