using System.Collections.Generic;

namespace Chat_Server_
{
    class Globals
    {
        public static List<IpAdress> players = new List<IpAdress>();
        public static string listOfLetters = "";
        public static int player1Score = 0;
        public static int player2Score = 0;

        public static void ResetGlobals()
        {
            players = new List<IpAdress>();
            listOfLetters = "";
            player1Score = 0;
            player2Score = 0;

        }
    }  
}
