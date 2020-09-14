using System.Collections.Generic;

namespace Chat_Server_
{
    class Globals
    {
        //Declaring all and initialising all global variables
        public static List<objectPlayer> players = new List<objectPlayer>();
        public static string listOfLetters = "";
        public static int player1Score = 0;
        public static int player2Score = 0;
        public static int count = 0;

        public static bool P1Played = false;
        public static bool P2Played = false;

        //Reset Global variables
        public static void ResetGlobals()
        {
            players = new List<objectPlayer>();
            listOfLetters = "";
            player1Score = 0;
            player2Score = 0;
            count = 0;
            P1Played = false;
            P2Played = false;

        }
    }  
}
