﻿using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chat_Client_
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            //create and start thread
            threadReceive = new Thread(new ThreadStart(ReceivedByClient));
            threadReceive.Start();
        }
        //============================================================Receive================================================================================
        Thread threadReceive;
        void ReceivedByClient()
        {
            Socket socketReceive = CreateSocketClient.ReceiveSocket();
            while (true)
            {
                Socket temp = null;
                try
                {
                    temp = socketReceive.Accept();
                    byte[] messageReceivedByServer = new byte[100];
                    temp.Receive(messageReceivedByServer, SocketFlags.None);
                    string str = Encoding.ASCII.GetString(messageReceivedByServer);

                    //identify player 1
                    if(str.Contains("Wait for the other player"))
                    {
                        GlobalClient.player1 = true;
                    }

                    string msg = ExtractMessage.PlayerId(str);

                    //start timer when receiving game start message
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
                temp.Close();
            }
            
        }

        System.Timers.Timer newTimer = new System.Timers.Timer();
        int counting = 5;
        public void send(object source, System.Timers.ElapsedEventArgs e)
           {
            //open form 2 when countdown finishes
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
            textBoxMessage.Enabled = false;
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string messageTextBox = textBoxMessage.Text;
            byte[] messageSentFromClient;
            try
            {
                //pass name and Ip of client
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST

                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                socketSend.Connect(GlobalClient.iPEndPointSend);
                messageSentFromClient = Encoding.ASCII.GetBytes(messageTextBox + "$" + myIP + "#");
                socketSend.Send(messageSentFromClient, SocketFlags.None);
                labelShow.Text = "Hi, " + messageTextBox;
                timeLabel.Text = "Please wait for the other player to connect";
                
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