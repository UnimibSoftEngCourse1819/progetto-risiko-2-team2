using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RiskServer1
{
    class ServerTCP
    {
        static TcpListener serversocket = new TcpListener(IPAddress.Any, 8899);

        public static void initializeNetwork()
        {
            Console.WriteLine("inizializzazione pacchetti ... ");
            ServerHandlData.InizializzaPacchetti();
            serversocket.Start();
            serversocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), null); // accetta un client

        }

        private static void OnClientConnect(IAsyncResult result)
        {
            TcpClient client = serversocket.EndAcceptTcpClient(result);// ogni nuova connessione usiamo la callback ce lo salviamo e passiamo al prosasimo
            serversocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), null); // accetta un client
            ClientManager.CreateNewConnection(client);
        }
    }
}
