using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat_Server_
{
    class CheckWord
    {
        public static string CheckExistingWord(String input)
        {
             
            int pos = input.IndexOf("$");
            string word = "";

            int endPos = input.IndexOf("#");
            string ipadd = "";

            //Extracting word from message sent from specific player
            for (int j = 0; j < pos; j++)
            {
                word += input.ElementAt(j);
            }
            for (int i = pos + 1; i < endPos; i++)
            {
                ipadd += input.ElementAt(i);

            }
            //Loading dictionary
            List<string> vwords = LoadWords.LoadWordsList();


            foreach (string words in vwords)
            {
                //Check if word exists in dictionary
                if (word.ToLower() == words.ToLower())
                {
                    //Add score using word length to corresponding player
                    if(ipadd.Equals(Globals.players[0].Ip))
                    {
                        Globals.player1Score += word.Length;
                    }
                    if (ipadd.Equals(Globals.players[1].Ip))
                    {
                        Globals.player2Score += word.Length;
                    }
                    return "true";
                }
            }
            //Return false if word does not exist
            return ("false"+ipadd).ToString();
        }
    }
}
