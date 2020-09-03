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
            List<string> vwords = LoadWords.LoadWordsList();
            Boolean found = false;
            foreach (string word in vwords)
            {
                if (input.ToLower() == word.ToLower())
                {
                    found = true;

                }
            }
            return found;
        }
    }
}
