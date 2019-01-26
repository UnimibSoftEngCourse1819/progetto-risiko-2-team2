using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.Server
{
    public enum serverPackets
    {
        sWelcomeMessage=1,
    }
   

     class DataReceiver  // dovrebbe essere static ma mi serve che non lo sia
    {
       

        public static void HandleWelcomeMessage(byte[] data) // dovrebbe essere static ma così funzoina
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.writeBytes(data);
            int playerID = buffer.ReadInteger();
            string msg = buffer.ReadString();
            buffer.Dispose();

            if (NetworkManager.state <= 0)  // casi normali 
            {
                switch (msg) // aggiungere un caso per scoprire il colore ?
                {
                    case "benvenuto nel server mettiti comodo": // mi sono connesso
                        {
                            Debug.Log(msg);
                            DataSender.SendHelloServer();
                            break;
                        }
                    /*   case "sei nella Wating room ora devi aspettare altri utenti":
                           {
                               Debug.Log(msg);                                           
                               break;
                           }
                   */
                    case "Come ti chiami ?":
                        {
                            Debug.Log("ecco come mi chiamo");
                            NetworkManager.state = -1; // impedisco l'uso del button nella waiting room
                            DataSender.SendName();
                            break;
                        }
                    case "ok":
                        {
                           NetworkManager.messaggio="ok";
                            Debug.Log("nome salvato");
                            NetworkManager.state = 0; // riattivo il bottone
                            break;
                        }
                /*    case "non si può giocare":
                        {
                            NetworkManager.messaggio = "no";
                            Debug.Log(msg);
                            break;
                        }
                    case "si puo giocare":
                        {
                            NetworkManager.messaggio = "si";
                            Debug.Log(msg);
                            break;
                        }
               */
                    case "startgame":
                        {
                            NetworkManager.messaggio = "start";
                            Debug.Log(msg);
                            break;
                        }
                    case "NewPlayer":
                        {
                            Debug.Log("newplayer");
                            NetworkManager.AddNPlayer(); // mi segno che è entrato un nuovo player
                            NetworkManager.messaggio = "NewPlayer";
                            break;
                        }
                    case "PlayerQuit":
                        {
                            Debug.Log("playerquit");
                            NetworkManager.DimNPlayer(); // mi segno che è uscito un nuovo player
                            NetworkManager.messaggio = "PlayerQuit";
                            break;
                        }

                }
            }
            else if(NetworkManager.state==1) // salvo il numero di player
            {
                NetworkManager.N_player = msg;
               // Debug.Log("msg");
                NetworkManager.state = 0;
              //  Debug.Log(NetworkManager.state);
            }
        }
    }
}
