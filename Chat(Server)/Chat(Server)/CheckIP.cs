using System;
using System.Linq;

namespace Chat_Server_
{

    class CheckIP
    {             
        public static void CheckIpAddress(string stringReceived)
        {
            int pos = stringReceived.IndexOf("$");
            int endPos = stringReceived.IndexOf("#");
            string ipadd = "";
            string name = "";
            for (int i = pos + 1; i < endPos; i++)
            {
                ipadd += stringReceived.ElementAt(i);
                
            }
            for (int j=0; j < pos; j++)
            {
                name += stringReceived.ElementAt(j);
            }
            //Console.WriteLine(Globals.players[0].Ip);
            if (name == "vowel" || name == "consonant")
            {
                Globals.listOfLetters += GenerateLetter.GenerateLetters(name);
                Console.WriteLine(Globals.listOfLetters);
            }

            if (Globals.players.Count < 2 && ipadd.Length != 0)
            {
               
                if (Globals.players.Count == 0)
                {
                    AddIp(ipadd, name);
                }
                else
                {
                    if (ipadd != Globals.players[0].Ip )
                    {
                        AddIp(ipadd, name);
                    }
                }
                
            }
            if (Globals.players.Count < 3){
                FormServer.count++;
            }
            
            for (int i = 0; i < Globals.players.Count; i++)
            {
                Console.WriteLine("Ip in list: "+Globals.players[i].Ip + Globals.players[i].Name);
            }
            Console.WriteLine();
        }

        public static void AddIp(string ip, string name)
        {
            objectPlayer playerip = new objectPlayer();
            playerip.Ip = ip;
            playerip.Name = name;
            Globals.players.Add(playerip);
            Console.WriteLine(playerip.Ip);
        }
    }
}
