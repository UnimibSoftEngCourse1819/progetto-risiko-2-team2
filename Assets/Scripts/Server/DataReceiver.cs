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
        sWelcomeMessage = 1,
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

            if (NetworkManager.getState() <= 0)  // casi normali 
            {
                switch (msg) // aggiungere un caso per scoprire il colore ?
                {
                    case "benvenuto nel server mettiti comodo": // mi sono connesso
                        {
                            Debug.Log(msg);
                            DataSender.SendHelloServer();
                            break;
                        }
                    case "Come ti chiami ?":
                        {
                            Debug.Log("ecco come mi chiamo");
                            NetworkManager.setState(-1); // impedisco l'uso del button nella waiting room
                            DataSender.SendName();
                            break;
                        }
                    case "ok":
                        {
                            NetworkManager.setMessage("ok");
                            Debug.Log("nome salvato");
                            NetworkManager.setState(0); // riattivo il bottone
                            break;
                        }
                    case "startgame":
                        {
                            NetworkManager.setMessage("start");
                            Debug.Log(msg);
                            break;
                        }
                    case "NewPlayer":
                        {
                            Debug.Log("newplayer");
                            NetworkManager.addNPlayer(); // mi segno che è entrato un nuovo player
                            NetworkManager.setMessage("NewPlayer");
                            break;
                        }
                    case "PlayerQuit":
                        {
                            Debug.Log("playerquit");
                            NetworkManager.dimNPlayer(); // mi segno che è uscito un nuovo player
                            NetworkManager.setMessage("PlayerQuit");
                            break;
                        }
                    case "Your Go":
                        {
                            NetworkManager.setMessage("Myturn");
                            break;
                        }
                    case "Attacco": // un player ha fatto un attacco devo aggionrare la mappa
                        {
                            NetworkManager.setState(3);
                            break;
                        }
                    case "Spostamento":// un player ha fatto uno spostamento devo aggionrare la mappa
                        {
                            NetworkManager.setState(4);
                            break;
                        }
                    case "Posizionamento":// un player ha fatto uno posizioanemtnopdevo aggionrare la mappa
                        {
                            NetworkManager.setState(5);
                            break;
                        }
                    case "Combo Carte":// un player ha fatto uno posizioanemtnopdevo aggionrare la mappa
                        {
                            NetworkManager.setState(6);
                            break;
                        }

                }
            }
            else if (NetworkManager.getState() == 1) // salvo il numero di player
            {
                NetworkManager.setNPlayer(int.Parse(msg));
                // Debug.Log("msg");
                NetworkManager.setState(0);
                //  Debug.Log(NetworkManager.state);
            }
            else if (NetworkManager.getState() == 2) // indico che mappa caricare
            {
                NetworkManager.setMap(int.Parse(msg));
                Debug.Log("starting map " + msg);
                NetworkManager.setState(0);
                //  Debug.Log(NetworkManager.state);
            }
            else if (NetworkManager.getState() == 3) // fase di attcacco 
            {
                NetworkManager.refresh(msg, 4);
            }
            else if (NetworkManager.getState() == 4) // fase di spostamento 
            {
                NetworkManager.refresh(msg, 3);
            }
            else if (NetworkManager.getState() == 5) // fase di posizioanemnto 
            {
                NetworkManager.refresh(msg, 1);
            }
            else if (NetworkManager.getState() == 6) // usata combo carte 
            {
                NetworkManager.refresh(msg, 2);
            }

        }
    }
}
