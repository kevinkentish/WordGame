using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client_
{
    class ExtractMessage
    {

        public static string PlayerId(String str)
        {

            int pos = 0;
            int endPos = str.IndexOf("!");
            int endPosName1 = str.IndexOf("@");
            int endPosName2 = str.IndexOf("%");
            string msg = "";

            //Retrieve player 1 and player 2 names + Message
            for (int i = pos; i <= endPos; i++)
            {
                msg += str.ElementAt(i);
            }

            //name of player 1
            for (int j = endPos + 1; j < endPosName1; j++)
            {
                GlobalClient.player1Name += str.ElementAt(j);
            }
            //name of player 2
            for (int k = endPosName1 + 1; k < endPosName2; k++)
            {
                GlobalClient.player2Name += str.ElementAt(k);
            }
            return msg;
        }
        public static void SetScores(String str, int endPos)
        {
            char[] tempScore1 = new char[10];
            char[] tempScore2 = new char[10];
            
            int halfPos = str.IndexOf("$");

            //Extract player 1 score
            for (int i = 0; i < halfPos; i++)
            {
                tempScore1[i] = str.ElementAt(i);

            }
            GlobalClient.player1score = new String(tempScore1);

            //Extract player 2 score
            for (int j = halfPos + 1; j < endPos; j++)
            {
                tempScore2[j - (halfPos + 1)] = str.ElementAt(j);
            }
            GlobalClient.player2score = new String(tempScore2);
        }
    }
}
