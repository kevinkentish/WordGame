using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client_
{
    class GlobalClient
    {

        public static int portSend = 40000;
        public static IPEndPoint iPEndPointSend = new IPEndPoint(IPAddress.Parse("10.232.20.230"), portSend);

        public static string player1score = "0";
        public static string player2score = "0";

        public static string player1Name= "";
        public static string player2Name = "";


        public static bool player1 = false;
        public static int roundPlayed = 0;

        public static void ResetGlobalsClient()
        {
            player1score = "0";
            player2score = "0";
            player1Name = "";
            player2Name = "";
            player1 = false;
            roundPlayed = 0;
        }
    }
}
