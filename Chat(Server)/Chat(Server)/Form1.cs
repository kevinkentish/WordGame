using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
                    ServerSend.SendToClient(Globals.players[i].Ip, "Game Start!", socketSend);
                    socketSend.Close();
                }

            }
        }

        public static void SendGeneratedStringOfLetters()
        {
            for (int i = 0; i < 2; i++)
            {
                Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ServerSend.SendToClient(Globals.players[i].Ip, (Globals.listOfLetters + "#"), socketSend);
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
        //================================================================================================================================================
        private void buttonSend_Click(object sender, EventArgs e)
        {
            //int portSend = 40001;
            //int portAkshit = 40002;

            //IPEndPoint iPEndPointSend = new IPEndPoint(IPAddress.Parse("10.232.20.230"), portSend);
            //IPEndPoint iPEndPointSendAkshit = new IPEndPoint(IPAddress.Parse("10.232.20.228"), portAkshit);

            //Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Socket socketSendAkshit = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string messageTextBox = textBoxMessage.Text;
            //byte[] messageSentFromServer;
            try
            {
                //socketSend.Connect(iPEndPointSend);
                //socketSendAkshit.Connect(iPEndPointSendAkshit);

                //messageSentFromServer = Encoding.ASCII.GetBytes(messageTextBox);

                //socketSend.Send(messageSentFromServer, SocketFlags.None);
                //socketSendAkshit.Send(messageSentFromServer, SocketFlags.None);

                //labelShow.Text += "\r\nServer: " + messageTextBox;
                //textBoxMessage.Text = null;
                String catchedMsg = string.Empty;
                
                for (int i = 0; i < 2; i++)
                {
                    Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);                  
                    catchedMsg = ServerSend.SendToClient(Globals.players[i].Ip, messageTextBox, socketSend);
                    Console.WriteLine(catchedMsg);
                    
                    textBoxMessage.Text = null;
                    socketSend.Close();
                }
                labelShow.Text += "\r\nServer: " + catchedMsg;
                //String catchedMsg = ServerSend.SendToClient(Globals.players[0].Ip, messageTextBox);
                //ServerSend.SendToClient(Globals.players[1].Ip, messageTextBox);

                //labelShow.Text += "\r\nServer: " + catchedMsg;
                //textBoxMessage.Text = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n" + ex.HelpLink + "\n" + ex.InnerException
                        + "\n" + ex.Source + "\n" + ex.TargetSite);
            }
            //finally
            //{
                //socketSend.Close();
                //socketSendAkshit.Close();
            //}
        }
        //============================================================Receive================================================================================
    }
}