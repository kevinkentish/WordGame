using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

namespace Chat_Client_
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            threadReceive = new Thread(new ThreadStart(ReceivedByClient));
            threadReceive.Start();
            Console.WriteLine("player1 name"+ GlobalClient.player1Name);
            Console.WriteLine("player1 score" + GlobalClient.player1score);
        }
        //============================================================Receive================================================================================
        Thread threadReceive;
        void ReceivedByClient()
        {
            Socket socketReceive = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int portReceive = 40001;
            IPEndPoint iPEndPointReceive = new IPEndPoint(IPAddress.Any, portReceive);
            socketReceive.Bind(iPEndPointReceive);
            socketReceive.Listen(10);
            while (true)
            {
                Socket temp = null;
                try
                {
                    temp = socketReceive.Accept();
                    byte[] messageReceivedByServer = new byte[100];
                    int sizeOfReceivedMessage = temp.Receive(messageReceivedByServer, SocketFlags.None);
                    string str = Encoding.ASCII.GetString(messageReceivedByServer);

                    if(str.Contains("Wait for the other player"))
                    {
                        GlobalClient.player1 = true;
                    }
                    int pos = 0;
                    int endPos = str.IndexOf("!");
                    int endPosName1 = str.IndexOf("@");
                    int endPosName2 = str.IndexOf("%");
                    string msg = "";
                    Console.WriteLine(str);

                    
                    for (int i = pos; i <= endPos; i++)
                    {
                        msg += str.ElementAt(i);
                    }

                    for (int j = endPos+1; j < endPosName1; j++)
                    {
                        GlobalClient.player1Name += str.ElementAt(j);
                    }

                    for (int k = endPosName1+1; k < endPosName2; k++)
                    {
                        GlobalClient.player2Name += str.ElementAt(k);
                    }
                    Console.WriteLine(msg);
                    Console.WriteLine(GlobalClient.player1Name);
                    Console.WriteLine(GlobalClient.player2Name);
                    
                    if (msg.Contains("Game Start!"))
                    {
                        socketReceive.Close();
                        newTimer.Enabled = true;
                        newTimer.Elapsed += new System.Timers.ElapsedEventHandler(send);
                        newTimer.Interval = 1000;

                        break;
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n" + ex.HelpLink + "\n" + ex.InnerException
                            + "\n" + ex.Source + "\n" + ex.TargetSite);
                }
                //finally
                //{
                //    temp.Close();
                //}
                temp.Close();
            }
            
        }

        System.Timers.Timer newTimer = new System.Timers.Timer();
        int counting = 5;
        public void send(object source, System.Timers.ElapsedEventArgs e)
           {
            if (counting == 0)
            {
                newTimer.Enabled = false;
                this.Dispose(true);
                Form2 frm = new Form2();
                frm.ShowDialog();
            }
            this.timeLabel.Text = "Game starting in ... "+counting.ToString();
            counting--;
            
        }
        //================================================================================================================================================
        private void buttonSend_Click(object sender, EventArgs e)
        {
            int portSend = 40000;
            IPEndPoint iPEndPointSend = new IPEndPoint(IPAddress.Parse("10.232.20.230"), portSend);
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string messageTextBox = textBoxMessage.Text;
            byte[] messageSentFromClient;
            try
            {
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST
                //Console.WriteLine(hostName);
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                socketSend.Connect(iPEndPointSend);
                messageSentFromClient = Encoding.ASCII.GetBytes(messageTextBox + "$" + myIP + "#");
                socketSend.Send(messageSentFromClient, SocketFlags.None);
                labelShow.Text = "Hi, " + messageTextBox;
                timeLabel.Text = "Please wait for the other player to connect";
                textBoxMessage.Text = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n" + ex.HelpLink + "\n" + ex.InnerException
                        + "\n" + ex.Source + "\n" + ex.TargetSite);
            }
            finally
            {
                socketSend.Close();
                buttonSend.Enabled = false;
            }
        }
        //============================================================Send================================================================================
    }
}