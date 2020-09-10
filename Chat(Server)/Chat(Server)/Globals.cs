using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server_
{
    class Globals
    {
        //Declaring all and initialising all global variables
        public static List<objectPlayer> players = new List<objectPlayer>();
        public static string listOfLetters = "";
        public static int player1Score = 0;
        public static int player2Score = 0;

        //Reset Global variables
        public static void ResetGlobals()
        {
            players = new List<objectPlayer>();
            listOfLetters = "";
            player1Score = 0;
            player2Score = 0;

        }
    }  
}
