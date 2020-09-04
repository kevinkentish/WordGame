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

namespace Chat_Client_
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            threadReceive = new Thread(new ThreadStart(ReceivedByClient));
            threadReceive.Start();
        }
        //============================================================Receive================================================================================
        Thread threadReceive;
        private void Consonant_Click(object sender, EventArgs e)
        {
            int portSend = 40000;
            IPEndPoint iPEndPointSend = new IPEndPoint(IPAddress.Parse("10.232.20.230"), portSend);
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string messageTextBox = "consonant";
            byte[] messageSentFromClient;
            try
            {
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST
                Console.WriteLine(hostName);
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                socketSend.Connect(iPEndPointSend);
                messageSentFromClient = Encoding.ASCII.GetBytes(messageTextBox + "$" + myIP + "#");
                socketSend.Send(messageSentFromClient, SocketFlags.None);
                //labelShow.Text += "\r\nClient: " + messageTextBox + myIP;
                //textBoxMessage.Text = null;
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

        private void Vowel_Click(object sender, EventArgs e)
        {
            int portSend = 40000;
            IPEndPoint iPEndPointSend = new IPEndPoint(IPAddress.Parse("10.232.20.230"), portSend);
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string messageTextBox = "vowel";
            byte[] messageSentFromClient;
            try
            {
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                socketSend.Connect(iPEndPointSend);
                messageSentFromClient = Encoding.ASCII.GetBytes(messageTextBox + "$" + myIP + "#");
                socketSend.Send(messageSentFromClient, SocketFlags.None);

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
        void ReceivedByClient()
        {
            Socket socketReceive = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int portReceive = 40001;
            IPEndPoint iPEndPointReceive = new IPEndPoint(IPAddress.Any, portReceive);
            socketReceive.Bind(iPEndPointReceive);
            socketReceive.Listen(10);
            string msg = "??????????";
            string temporaryString = "";
            char[] ch = msg.ToCharArray();
            string player1Score="";
            string player2Score = "";

            while (true)
            {
                Socket temp = null;
                try
                {
                    temp = socketReceive.Accept();
                    byte[] messageReceivedByServer = new byte[100];
                    int sizeOfReceivedMessage = temp.Receive(messageReceivedByServer, SocketFlags.None);
                    string str = Encoding.ASCII.GetString(messageReceivedByServer);
                    char[] tempScore1 = new char[10];
                    char[] tempScore2 = new char[10];

                    Console.WriteLine(str);

                    int endPos = str.IndexOf("#");
                    
                    Console.WriteLine(str);
                    
                    if (str.Contains("$"))
                    {
                        int halfPos = str.IndexOf("$");
                        for (int i = 0; i < halfPos; i++)
                        {
                            tempScore1[i] = str.ElementAt(i);
                            
                        }
                        player1Score = new String(tempScore1);
                        for (int j = halfPos+1; j < endPos; j++)
                        {
                            int i = 0;
                            tempScore2[i] = str.ElementAt(j);
                            i++;
                        }
                        player2Score = new String(tempScore2);
                    }
                    else
                    {
                        for (int i = 0; i < endPos; i++)
                        {
                            ch[i] = str.ElementAt(i);

                        }
                        temporaryString = new String(ch);
                        button1.Text = temporaryString.ElementAt(0).ToString();
                        button2.Text = temporaryString.ElementAt(1).ToString();
                        button3.Text = temporaryString.ElementAt(2).ToString();
                        button4.Text = temporaryString.ElementAt(3).ToString();
                        button5.Text = temporaryString.ElementAt(4).ToString();
                        button6.Text = temporaryString.ElementAt(5).ToString();
                        button7.Text = temporaryString.ElementAt(6).ToString();
                        button8.Text = temporaryString.ElementAt(7).ToString();
                        button9.Text = temporaryString.ElementAt(8).ToString();
                        button10.Text = temporaryString.ElementAt(9).ToString();
                        Console.WriteLine(msg);
                    }
                    
                    //labelShow.Text += "\r\nServer: " + str;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n" + ex.HelpLink + "\n" + ex.InnerException
                            + "\n" + ex.Source + "\n" + ex.TargetSite);
                }
                finally
                {
                    temp.Close();
                    Player1ScoreLabel.Text = player1Score;
                    Player2ScoreLabel.Text = player2Score;
                }
            }
        }

        private void button10_TextChanged(object sender, EventArgs e)
        {
            foreach (Button s in Controls.OfType<Button>())
            {
                s.Enabled = true;
                s.UseVisualStyleBackColor = true;
            }
            Consonant.Enabled = false;
            Vowel.Enabled = false;
        }
        void MyButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            word.Text += button.Text;
            button.Enabled = false;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            int portSend = 40000;
            IPEndPoint iPEndPointSend = new IPEndPoint(IPAddress.Parse("10.232.20.230"), portSend);
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string messageTextBox = word.Text; ;
            byte[] messageSentFromClient;
            try
            {
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                socketSend.Connect(iPEndPointSend);
                messageSentFromClient = Encoding.ASCII.GetBytes(messageTextBox + "$" + myIP + "#");
                socketSend.Send(messageSentFromClient, SocketFlags.None);

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
    }
}
