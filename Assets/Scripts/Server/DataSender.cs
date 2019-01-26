﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Server
{
    public enum ClientPackets
    {
        cHelloServer=1,
    }

    static  class DataSender
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
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
        }
        public static void StarGame()
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((int)ClientPackets.cHelloServer);
            buffer.WriteString("start game");
            ClientTcp.SendData(buffer.ToArray());
            buffer.Dispose();
        }

    }
}
