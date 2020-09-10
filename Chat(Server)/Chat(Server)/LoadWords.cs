using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Chat_Server_
{
    class LoadWords
    {
        public static List<string> LoadWordsList()
        {
            List<string> ValidWords = new List<string>();
            //location of the dictonary
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources\words.txt");

            //Adding each word from the txt file to a List
            foreach (var w in File.ReadAllLines(path))
            {
                ValidWords.Add(w);
            }

            return ValidWords;
        }
    }
}
