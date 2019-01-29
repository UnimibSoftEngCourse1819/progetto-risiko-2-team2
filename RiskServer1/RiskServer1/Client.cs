using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RiskServer1
{
    class Client
    {
        public int COnnectionID;
        public TcpClient socket;
        public NetworkStream stream;
        public byte[] reciverbuffer;
        public ByteBuffer buffer;

        public void Start()
        {
            socket.SendBufferSize = 4096; // grandezza pacchetti
            socket.ReceiveBufferSize = 4096;
            stream = socket.GetStream();
            reciverbuffer = new byte[4096];
            stream.BeginRead(reciverbuffer, 0, socket.ReceiveBufferSize, OnReceiveData, null);
            Console.WriteLine("connessione in arrivo da '{0}'.", socket.Client.RemoteEndPoint.ToString());
        }

        private void OnReceiveData(IAsyncResult result)
        {
            try
            {
                int length = stream.EndRead(result);
                if (length <= 0)
                {
                    CloseConnection();
                    return;
                }
                byte[] newbytes = new byte[length];
                Array.Copy(reciverbuffer, newbytes, length);
                ServerHandlData.HandleData(COnnectionID, newbytes);
                stream.BeginRead(reciverbuffer, 0, socket.ReceiveBufferSize, OnReceiveData, null);

            }
            catch (Exception)
            {
                return;
            }
        }

        private void CloseConnection()
        {
            Console.WriteLine("la connessione è stata terminata");
            socket.Close();
        }
    }
}
