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
            Socket socketReceive = CreateSocketServer.ReceiveSocket();

            while (true)
            {
               //Create temporary socket to receive messages from clients
                Socket temp = null;
                try
                {
                    temp = socketReceive.Accept();
                    byte[] messageReceivedByServer = new byte[100];
                    int sizeOfReceivedMessage = temp.Receive(messageReceivedByServer, SocketFlags.None);
                    string str = Encoding.ASCII.GetString(messageReceivedByServer);
                    CheckIP.CheckIpAddress(str);

                    //Display message received from clients in server label.
                    labelShow.Text += "\r\n Client: " + str;

                    if (str.ElementAt(str.IndexOf("^") + 2).Equals('1'))
                    {
                        Globals.P1Played = true;
                    }
                    if (str.ElementAt(str.IndexOf("^") + 2).Equals('2'))
                    {
                        Globals.P2Played = true;
                    }


                    if (Globals.players.Count < 2)
                    {
                        Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        ServerSend.SendToClient(Globals.players[0].Ip, "Wait for the other player", socketSend);
                    }
                    if (Globals.count == 2)
                    {
                        PlayGame.GameStart();
                    }
                    
                    if (Globals.count > 2 && Globals.count < 13)
                    {
                        PlayGame.SendGeneratedStringOfLetters();
                    }
                    if (Globals.count > 12 && Globals.count < 15 )
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
                        if(Globals.count == 14)
                        {
                            PlayGame.SendScores();
                        }
                        
                    }
                    if (str.Contains("!Reset!"))
                    {
                        Globals.count = 2;
                        Globals.listOfLetters = "";
                        PlayGame.SendGeneratedStringOfLetters();

                        for (int i = 0; i < 2; i++)
                        {
                            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            string catchedMsg = ServerSend.SendToClient(Globals.players[i].Ip, "!Reset!", socketSend);
                            
                            socketSend.Close();
                        }

                    }
                    
                    if (str.ElementAt(str.IndexOf("#") + 1).Equals('4'))
                        {
                        Console.WriteLine("P1 Played tag: " + Globals.P1Played);
                        Console.WriteLine("P2 Played tag: " + Globals.P2Played);

                        if (Globals.P1Played && Globals.P2Played)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                string catchedMsg = ServerSend.SendToClient(Globals.players[i].Ip, "!DisplayScore!", socketSend);
                                Console.WriteLine(catchedMsg);

                                socketSend.Close();
                            }
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