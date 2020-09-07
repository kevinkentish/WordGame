using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            for (int j = 0; j < pos; j++)
            {
                word += input.ElementAt(j);
            }
            for (int i = pos + 1; i < endPos; i++)
            {
                ipadd += input.ElementAt(i);

            }
            Console.WriteLine(ipadd);
            Console.WriteLine(word);
            List<string> vwords = LoadWords.LoadWordsList();
            Console.WriteLine(Globals.players[0].Ip);
            foreach (string words in vwords)
            {
                if (word.ToLower() == words.ToLower())
                {
                    if(ipadd.Equals(Globals.players[0].Ip))
                    {
                        Globals.player1Score += word.Length;
                        Console.WriteLine(Globals.player1Score);
                    }
                    if (ipadd.Equals(Globals.players[1].Ip))
                    {
                        Globals.player2Score += word.Length;
                        Console.WriteLine(Globals.player2Score);
                    }
                    return "true";
                }
            }
            return ("false"+ipadd).ToString();
        }
    }
}
