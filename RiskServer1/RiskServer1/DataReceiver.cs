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

            if (GameManager.GetState() == 0) {
                switch (msg)
                {
                    case "ora sono connesso al server": // invio ai player la notifica del nuovo entrato
                        { // sincronizzo il numero di player attivi
                            Console.WriteLine(msg+ " "+connectionID);
                            GameManager.SetState(1);
                            if (ClientManager.client.Count > 1)
                            {
                                foreach (KeyValuePair<int, Client> keyValue in ClientManager.client)
                                {
                                    int c = keyValue.Key;
                                    if (c!=connectionID)
                                    {
                                        DataSender.SendNewPlayer(keyValue.Key);                         
                                        Console.WriteLine("+1 per chi è gia connesso");
                                    }
                                  
                                }
                            }
                            Console.WriteLine("chiedo il nome");
                            DataSender.SendAskName(connectionID);
                            //     DataSender.SendWaitingMessage(connectionID); 
                            break;
                        }
                    case "addio":  // elimino il player e notifico tutti
                        {
                            Console.WriteLine("addio "+connectionID);
                            ClientManager.DeleteRecord(connectionID);
                            // se accade in game setto il player a inattivo
                            foreach (KeyValuePair<int, Client> keyValue in ClientManager.client)
                            {
                                DataSender.SendPlayerQuit(keyValue.Key);
                            }
                            break;
                        }
                    case "quanti player sono connessi ?":
                        {
                            Console.WriteLine(msg+" "+connectionID);
                            DataSender.SendNumPlayer(connectionID);
                            break;
                        }
                    case "start game": // mando un broadcast ai player
                        {
                            Console.WriteLine(msg);                          
                            foreach (KeyValuePair<int, Client> keyValue in ClientManager.client)
                            {
                                Console.WriteLine("starting new player ...");
                                DataSender.SendStartGame(keyValue.Key);
                            }
                            System.Threading.Thread.Sleep(2000);
                            GameManager.StartGame(); // faccio partire il gioco
                            break;
                        }
                    case "Passo":
                        {

                            break;
                        }
                }
            }
            else if(GameManager.GetState()==1) // per salvare il nome del player
            {
                GameManager.SetState(0);
                Console.WriteLine("salvataggio player");
                GameManager.AddPlayer(connectionID, msg);
                Console.WriteLine("player "+connectionID+" saved!");
                DataSender.SendOk(connectionID);
            }
            
        }
    }
}
