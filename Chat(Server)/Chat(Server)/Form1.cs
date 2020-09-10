using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Chat_Server_
{
    public partial class FormServer : Form
    {
        public static int count = 0;
        public static void GameStart()
        {
            if (Globals.players.Count == 2)
            {
                
                for (int i = 0; i < 2; i++)
                {
                    Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ServerSend.SendToClient(Globals.players[i].Ip, "Game Start!"+Globals.players[0].Name + "@"+Globals.players[1].Name + "%", socketSend);
                    socketSend.Close();
                }

            }
        }

        public static void SendGeneratedStringOfLetters()
        {
            String playflag = "";
            int play = count % 2;            
            for (int i = 0; i < 2; i++)
            {
                playflag = "!wait!";
                Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (i==0)
                {
                    if (play == 0)
                    { 
                        playflag = "!play!";
                        ServerSend.SendToClient(Globals.players[i].Ip, (Globals.listOfLetters + "#"+ playflag), socketSend);
                    } else
                    {
                        ServerSend.SendToClient(Globals.players[i].Ip, (Globals.listOfLetters + "#" + playflag), socketSend);
                    }
                   
                }
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

        public static void SendScores()
        {
            for (int i = 0; i < 2; i++)
            {
                Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ServerSend.SendToClient(Globals.players[i].Ip, (Globals.player1Score + "$" + Globals.player2Score+"#"), socketSend);
                socketSend.Close();
            }
        }
        public FormServer()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            
            threadReceive = new Thread(new ThreadStart(ReceivedByServer));
            threadReceive.Start();
        }
        //============================================================Receive================================================================================
        Thread threadReceive;
        void ReceivedByServer()
        {
            Socket socketReceive = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int portReceive = 40000;
            IPEndPoint iPEndPointReceive = new IPEndPoint(IPAddress.Any, portReceive);
            socketReceive.Bind(iPEndPointReceive);
            socketReceive.Listen(10);
            String client = String.Empty;
            while (true)
            {
                Socket temp = null;
                try
                {

                    //Put in a class
                    temp = socketReceive.Accept();
                    byte[] messageReceivedByServer = new byte[100];
                    int sizeOfReceivedMessage = temp.Receive(messageReceivedByServer, SocketFlags.None);
                    string str = Encoding.ASCII.GetString(messageReceivedByServer);
                    CheckIP.CheckIpAddress(str);

                    labelShow.Text += "\r\nClient: "+ client + str;
                    if (Globals.players.Count < 2)
                    {
                        Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        ServerSend.SendToClient(Globals.players[0].Ip, "Wait for the other player", socketSend);
                    }
                    if (count == 2)
                    {
                        GameStart();
                    }
                    
                    if (count > 2 && count < 13)
                    {
                        SendGeneratedStringOfLetters();
                    }
                    if (count > 12 && count<15 )
                    {
                        string found = CheckWord.CheckExistingWord(str);
                        //contains false send word does not exist 
                        Console.WriteLine(found.ToString());
                        if (found.Contains("false"))
                        {
                            int endPos = found.IndexOf("e");
                            string userIP = "";
                            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            for (int i = endPos + 1; i < found.Length; i++)
                            {
                                userIP += found.ElementAt(i);   
                            }
                            ServerSend.SendToClient(userIP, "!Invalid!", socketSend);
                        }
                        if(count == 14)
                        {
                            SendScores();
                        }
                        
                    }
                    if (str.Contains("!Reset!"))
                    {
                        count = 2;
                        Globals.listOfLetters = "";
                        SendGeneratedStringOfLetters();

                        for (int i = 0; i < 2; i++)
                        {
                            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            string catchedMsg = ServerSend.SendToClient(Globals.players[i].Ip, "!Reset!", socketSend);
                            Console.WriteLine(catchedMsg);
                            
                            socketSend.Close();
                        }

                    }
                    if (str.Contains("!CheckResults!"))
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            string catchedMsg = ServerSend.SendToClient(Globals.players[i].Ip, "!Reset!", socketSend);
                            Console.WriteLine(catchedMsg);

                            socketSend.Close();
                        }

                    }
                    if (str.Contains("RestartServer"))
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            string catchedMsg = ServerSend.SendToClient(Globals.players[i].Ip, "!RestartClient!", socketSend);
                            Console.WriteLine(catchedMsg);

                            socketSend.Close();
                        }
                        Globals.ResetGlobals();
                        count = 0;
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n" + ex.HelpLink + "\n" + ex.InnerException
                            + "\n" + ex.Source + "\n" + ex.TargetSite);
                }
                finally
                {
                    temp.Close();
                }
            }
        }
        //============================================================Receive================================================================================
    }
}