using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server_
{
    class ServerSend
    {
        public static string SendToClient(string ipAddress, string message,Socket socketSend)
        {
            int port = 40001;

            IPEndPoint iPEndPointSend = new IPEndPoint(IPAddress.Parse(ipAddress), port);

            byte[] messageSentFromServer;
            socketSend.Connect(iPEndPointSend);

            messageSentFromServer = Encoding.ASCII.GetBytes(message);

            socketSend.Send(messageSentFromServer, SocketFlags.None);

            return message;

        }
    }
}
