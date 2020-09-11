using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server_
{
    class PlayGame
    {

        //Start the game when both players are connected
        public static void GameStart()
        {
            //Check is both players are in list of players
            if (Globals.players.Count == 2)
            {

                for (int i = 0; i < 2; i++)
                {
                    Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ServerSend.SendToClient(Globals.players[i].Ip, "Game Start!" + Globals.players[0].Name + "@" + Globals.players[1].Name + "%", socketSend);
                    socketSend.Close();
                }

            }
        }

        //Sends generated letter when it is the corresponding players turn to choose.
        public static void SendGeneratedStringOfLetters()
        {
            String playflag = "";
            int play = Globals.count % 2;
            for (int i = 0; i < 2; i++)
            {
                 
                playflag = "!wait!";
                Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //Send to player 1
                if (i == 0)
                {
                    if (play == 0)
                    {
                        playflag = "!play!";
                        ServerSend.SendToClient(Globals.players[i].Ip, (Globals.listOfLetters + "#" + playflag), socketSend);
                    }
                    else
                    {
                        ServerSend.SendToClient(Globals.players[i].Ip, (Globals.listOfLetters + "#" + playflag), socketSend);
                    }

                }

                //Send to player 2
                if (i == 1)
                {
                    if (play == 1)
                    {
                        playflag = "!play!";
                        ServerSend.SendToClient(Globals.players[i].Ip, (Globals.listOfLetters + "#" + playflag), socketSend);
                    }
                    else
                    {
                        ServerSend.SendToClient(Globals.players[i].Ip, (Globals.listOfLetters + "#" + playflag), socketSend);
                    }
                }
                socketSend.Close();
            }
        }

        //Send both players their scores.
        public static void SendScores()
        {
            for (int i = 0; i < 2; i++)
            {
                Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ServerSend.SendToClient(Globals.players[i].Ip, (Globals.player1Score + "$" + Globals.player2Score + "#"), socketSend);
                socketSend.Close();
            }
        }


    }
}
