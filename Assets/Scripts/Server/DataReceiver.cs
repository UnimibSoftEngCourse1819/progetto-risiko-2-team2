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

           
            switch (msg)
            {
                case "benvenuto nel server mettiti comodo": // mi sono connesso
                    {
                        Debug.Log(msg);
                        DataSender.SendHelloServer();
                        break;
                    }
                case "sei nella Wating room ora devi aspettare altri utenti":
                    {
                        Debug.Log(msg);                                           
                        break;
                    }
                case "non si può giocare":
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
                case "startgame":
                    {
                        NetworkManager.messaggio = "start";
                        Debug.Log(msg);
                        break;
                    }
                     
            }
        }
    }
}
