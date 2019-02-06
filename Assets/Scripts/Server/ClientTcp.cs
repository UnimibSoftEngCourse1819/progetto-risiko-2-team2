using System;
using System.Net.Sockets;
using UnityEngine;

namespace Server
{
     static class ClientTcp  // dovrebbe essere static ma mi serve che non lo sia 
    {
        private static TcpClient clientSocket;
        private static NetworkStream myStream;
        private static byte[] recBuffer;
        

        public static void InizializzaNetwork()
        {
            clientSocket = new TcpClient();
            clientSocket.ReceiveBufferSize = 4096;
            clientSocket.SendBufferSize = 4096;
            recBuffer = new byte[4096 * 2];
            clientSocket.BeginConnect("127.0.0.1", 8899,new AsyncCallback(ClientConnectCallBack),clientSocket);
            

        }
        private static void ClientConnectCallBack(IAsyncResult result)
        {
            clientSocket.EndConnect(result);
            if (clientSocket.Connected )
            {
                clientSocket.NoDelay = true;
                myStream = clientSocket.GetStream();
                myStream.BeginRead(recBuffer, 0, 4096 * 2, ReceiveCallBack, null);

            }
        }
        private static  void ReceiveCallBack(IAsyncResult result)
        {
            try
            {
                int length = myStream.EndRead(result);
                if (length <= 0)
                {
                    return;
                }
                byte[] newbytes = new byte[length];
                Array.Copy(recBuffer, newbytes, length);
                UnityThread.executeInFixedUpdate(() =>
                {
                    ClientHandleData.HandleData(newbytes);
                });
                myStream.BeginRead(recBuffer, 0, 4096 * 2, ReceiveCallBack, null);
            }
            catch (Exception)
            {
                Debug.Log("Error!");
            }
        }
       public static void SendData(byte[] data)
        {

            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger(data.GetUpperBound(0) - data.GetLowerBound(0) + 1);
            buffer.writeBytes(data);
            myStream.BeginWrite(buffer.ToArray(), 0, buffer.Length(), null, null);
            buffer.Dispose();
        }
        public static void Disconnect()
        {
            clientSocket.Close();
        }

    }
}
