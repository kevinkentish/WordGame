using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server_
{
    class CheckWord
    {
        public static Boolean CheckExistingWord(String input)
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
            Boolean found = false;
            Console.WriteLine(Globals.players[0].Ip);
            foreach (string words in vwords)
            {
                if (word.ToLower() == words.ToLower())
                {
                    Console.WriteLine("hun");
                    if(ipadd.Equals(Globals.players[0].Ip))
                    {
                        Console.WriteLine("p1");
                        Globals.player1Score += word.Length;
                    }
                    else if (ipadd.Equals(Globals.players[1].Ip))
                    {
                        Console.WriteLine("p2");
                        Globals.player2Score += word.Length;
                    }
                    return found = true;
                }
            }
            return found;
        }
    }
}
