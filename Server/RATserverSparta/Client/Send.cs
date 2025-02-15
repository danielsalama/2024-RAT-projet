﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RATserverSparta.Client
{
    public class SocketSend
    {
        private Socket ClientSocket;
        public SocketSend(Socket clientSocket)
        {
            this.ClientSocket = clientSocket;
        }
        public dynamic Send(byte[] message, byte messageType)
        {
            if (message.Length > 0)
            {
                try
                {
                    byte[] type = new byte[] { messageType };
                    byte[] mesLength = BitConverter.GetBytes(message.Length);
                    byte[] tlv = new byte[1 + 5 + message.Length];

                    Array.Copy(type, 0, tlv, 0, 1);
                    Array.Copy(mesLength, 0, tlv, 1, 4);
                    Array.Copy(message, 0, tlv, 6, message.Length);

                    ClientSocket.Send(tlv);
                }
                catch { return false; }
            }
            return true;
        }
    }
}
