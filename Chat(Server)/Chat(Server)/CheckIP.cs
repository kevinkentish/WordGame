using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

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
            for(int j=0; j < pos; j++)
            {
                name += stringReceived.ElementAt(j);
            }
            if (ipadd.Length != 0)
            {
                IpAdress playerip = new IpAdress();
                playerip.Ip = ipadd;
                playerip.Name = name;
                Globals.players.Add(playerip);
                Console.WriteLine(playerip.Ip);
                for(int i=0; i < Globals.players.Count; i++)
                {
                    Console.WriteLine(Globals.players[i].Ip + Globals.players[i].Name);
                }
            }  
        }
    }
}
