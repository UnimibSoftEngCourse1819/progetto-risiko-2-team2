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
                            GameManager.SetState(2); // mi preparo a ricevere il numero dell amappa

                            break;
                        }

                    case "Passo": // si passsa il turno 
                        {
                            GameManager.SetState(8); // stato di passo = statop next phase
                            Console.WriteLine("attacco");
                            break;
                        }
                    case "Attacco":
                        {
                            GameManager.SetState(3); // stato di attacco
                            Console.WriteLine("attacco");
                            break;
                        }
                    case "Spostamento":
                        {
                            GameManager.SetState(4); // stato spostamento
                            Console.WriteLine("spostamento");
                            break;
                        }
                      case"Posizionamento":
                        {
                            GameManager.SetState(5); // stato spostamento
                            Console.WriteLine("Posizionamento");
                            break;
                        }
                    case "Combo Carte":
                        {
                            GameManager.SetState(6); // stato spostamento
                            Console.WriteLine("Combo carte");
                            break;
                        }
                    case"Dichiaro Attacco":
                        {
                            GameManager.SetState(7); // stato spostamento
                            Console.WriteLine("Dichiaro attacco");
                            break;
                        }
                    case "Next Phase":
                        {
                            GameManager.SetState(8); // stato spostamento
                            Console.WriteLine("Next Phase");
                            break;
                        }
                    case "Nomi Player":
                        {
                            Console.WriteLine("Next Phase");
                            GameManager.GestioneGameMessages("", connectionID, 7); // 
                            break;
                        }

                }
            }
            else if(GameManager.GetState()==1) // per salvare il nome del player
            {
                GameManager.SetState(0);
                Console.WriteLine("salvataggio player");
                GameManager.AddPlayer(connectionID, msg);
                Console.WriteLine("player "+connectionID+" "+msg+ "saved!");
                DataSender.SendOk(connectionID);
            }
            else if(GameManager.GetState() == 2) // per inviare il nome della mappa da giocare
            {
                GameManager.SetState(0);
                foreach (KeyValuePair<int, Client> keyValue in ClientManager.client)
                {
                    Console.WriteLine("starting map ..."+msg);
                    DataSender.SendStartGame(keyValue.Key,msg);
                }

            }
            else if(GameManager.GetState()==3) // mod attacco
            {
                GameManager.GestioneGameMessages(msg,connectionID,4);                
            }
            else if (GameManager.GetState() == 4) // mod spostamento
            { 
                GameManager.GestioneGameMessages(msg,connectionID,3);              
            }
            else if (GameManager.GetState() == 5) // mod posizionamento
            {
                GameManager.GestioneGameMessages(msg, connectionID,1);
            }
            else if (GameManager.GetState() == 6) // mod combo carte
            {
                GameManager.GestioneGameMessages(msg, connectionID,2); // ri utilizzo lo stesso codice
            }
            else if (GameManager.GetState() == 7) // attacco dichiarato
            {
                GameManager.GestioneGameMessages(msg, connectionID, 5); // ri utilizzo lo stesso codice
            }
            else if (GameManager.GetState() == 8) // next phase
            {
                GameManager.GestioneGameMessages(msg, connectionID, 6); // ri utilizzo lo stesso codice
            }
           
        }
    }
}