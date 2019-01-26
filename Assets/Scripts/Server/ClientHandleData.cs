using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Server
{
   static  class ClientHandleData
    {
        private static ByteBuffer playerBuffer;
        public delegate void Packet( byte[] data); // legge tutti i dati sulla network
        public static Dictionary<int, Packet> packets = new Dictionary<int, Packet>();

        public static void InizializzaPacchetti()
        {
            packets.Add((int)serverPackets.sWelcomeMessage, DataReceiver.HandleWelcomeMessage);
        }

        public static void HandleData(byte[] data)// gestisce la perdita di pacchetti
        {
            byte[] buffer = (byte[])data.Clone();
            int plength = 0;

            if (playerBuffer == null)
                playerBuffer = new ByteBuffer();

            playerBuffer.writeBytes(buffer);
            if (playerBuffer.Count() == 0) // il messaggio era vuoto
            {
                playerBuffer.Clear();
                return;
            }
            if (playerBuffer.Length() >= 4) // il messaggio non era un integer
            {
                plength = playerBuffer.ReadInteger(false);
                if (plength <= 0)
                {
                    playerBuffer.Clear();
                    return;
                }
            }
            while (plength >= 0 && plength <= playerBuffer.Length() - 4) // usiamo gli int come packetidentifier 
            {
                if (plength <= playerBuffer.Length() - 4)
                {
                    playerBuffer.ReadInteger();
                    data = playerBuffer.ReadBytes(plength);
                    HandleDataPackets(data);
                }
                plength = 0;
                if (playerBuffer.Length() >= 4)
                {
                    plength = playerBuffer.ReadInteger(false);
                    if (plength <= 0)
                    {
                        playerBuffer.Clear();
                        return;
                    }
                }
            }
            if (plength <= 1)
            {
                playerBuffer.Clear();
            }
        }

        private static void HandleDataPackets( byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.writeBytes(data);
            int packetID = buffer.ReadInteger();
            buffer.Dispose();
            if (packets.TryGetValue(packetID, out Packet packet))
            {
                packet.Invoke( data);
            }
        }
    }
}
