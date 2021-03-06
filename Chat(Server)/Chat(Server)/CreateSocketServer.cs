﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server_
{
    class CreateSocketServer
    {
        public static Socket ReceiveSocket() {  
            Socket socketReceive = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int portReceive = 40000;
            IPEndPoint iPEndPointReceive = new IPEndPoint(IPAddress.Any, portReceive);
            socketReceive.Bind(iPEndPointReceive);
            socketReceive.Listen(10);
            return socketReceive;
        }
    }
}
