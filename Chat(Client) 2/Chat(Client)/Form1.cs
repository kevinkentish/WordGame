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
                    labelShow.Text += "\r\nServer: " + str;
                    int pos = 0;
                    int endPos = str.IndexOf("!");
                    string msg = "";
                    Console.WriteLine(str);

                    
                    for (int i = pos; i <= endPos; i++)
                    {
                        msg += str.ElementAt(i);
                    }
                    Console.WriteLine(msg);
                    if (msg.Equals("Game Start!"))
                    {
                        socketReceive.Close();
                        temp.Close();
                        
                        this.Close();
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
        //================================================================================================================================================
        private void buttonSend_Click(object sender, EventArgs e)
        {
            int portSend = 40000;
            IPEndPoint iPEndPointSend = new IPEndPoint(IPAddress.Parse("192.168.100.109"), portSend);
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
                labelShow.Text += "\r\nClient: " + messageTextBox + myIP;
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
            }
        }
        //============================================================Send================================================================================
    }
}