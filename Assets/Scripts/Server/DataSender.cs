﻿using System.Linq;

namespace Server
{
    public enum ClientPackets
    {
        cHelloServer = 1,
    }

    static class DataSender
    {
        public static void SendHelloServer()
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("ora sono connesso al server");
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
        }
        public static void AskNPlayer()
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("quanti player sono connessi ?");
            NetworkManager.setState(1);
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
        }
        public static void StartMap(int a) // in via la pasrtenza del game e quale mappa
        {

            ByteBuffer buffer = new ByteBuffer();

                     
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString(a.ToString());
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
        }
        public static void StarGame(int a) // in via la pasrtenza del game e quale mappa
        {
            NetworkManager.setState(2);
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("start game");
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
            DataSender.StartMap(a);
        }
        public static void SendName()
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString(NetworkManager.getMessage()); // invio il nome
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
        }
        public static void SendGoodBye() // quando si chiude l'applicazione
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("addio"); // 
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
        }

        public static void SendPasso(string msg) // quando si passa il turno
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("Passo"); // 
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
            SendData(msg);
        }
        //attacco
        /***********************************************************************/
        public static void SendAttacco(string msg) // quando si attacca gli si passano i due stati coinvolti e il risultato
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("Attacco"); // 
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
            SendData(msg);

        }
        public static void SendData(string msg) //
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString(msg); // 
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
        }
        public static void SendSpostamento(string msg) // quando si sposta gli si passano 2 stati e il numero di armate
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("Spostamento"); // 
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
            SendData(msg);

        }
        public static void SendPosizionamento(string msg) // quando si posiziona gli si passa 1 stati e il numero di armate
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("Posizionamento"); // 
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
            SendData(msg); // e gli passo lo stato
        }
        public static void SendComboCarte(string msg) // quando si posiziona gli si passa 1 stati e il numero di armate
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("Combo Carte"); // 
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
            SendData(msg);

        }
        public static void SendAttackDeclared(string msg) // dichiarato un attacco
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("Dichiaro Attacco"); // 
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
            SendData(msg);
        }
        public static void SendnextPhaseLoad(string msg) // dichiarato un attacco
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("Next Phase"); // 
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
            SendData(msg);
        }
        public static void SendAskNamePlayer() // dichiarato un attacco
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("Nomi Player"); // 
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
            
        }

    }
}
