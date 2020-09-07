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
        public static int control = 0;
        public Form2()
        {
            InitializeComponent();
            if (GlobalClient.player1)
            {
                Vowel.Enabled = true;
                Consonant.Enabled = true;
            }
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
                // Retrive the Name of HOST
                string hostName = Dns.GetHostName();
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
                // Retrive the Name of HOST
                string hostName = Dns.GetHostName();
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
        public void ReceivedByClient()
        {
            if (control == 1)
            {
                control = 0;
            }
            Console.WriteLine("Kentish");
            Socket socketReceive = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int portReceive = 40001;
            IPEndPoint iPEndPointReceive = new IPEndPoint(IPAddress.Any, portReceive);
            socketReceive.Bind(iPEndPointReceive);
            socketReceive.Listen(10);
            string msg = "??????????";
            string temporaryString = "";
            char[] ch = msg.ToCharArray();
            
            Console.WriteLine("Client avant while loop: " + control);

            //bool controlFlag = true;
            while (control==0)
            {
                Socket temp = null;
                Player1ScoreLabel.Text = GlobalClient.player1score;
                Player2ScoreLabel.Text = GlobalClient.player2score;
                try
                {


                    if (control == 0)
                    {
                        Console.WriteLine("Client zis avant temp socketAccept <COUNT>: " + control);
                        temp = socketReceive.Accept();
                        Console.WriteLine("apres temp00");

                        byte[] messageReceivedByServer = new byte[100];
                        int sizeOfReceivedMessage = temp.Receive(messageReceivedByServer, SocketFlags.None);
                        string str = Encoding.ASCII.GetString(messageReceivedByServer);
                        char[] tempScore1 = new char[10];
                        char[] tempScore2 = new char[10];

                        //Console.WriteLine(str);
                        if (str.Contains("!play!"))
                        {
                            Consonant.Enabled = true;
                            Vowel.Enabled = true;
                        }
                        if (str.Contains("!wait!"))
                        {
                            Consonant.Enabled = false;
                            Vowel.Enabled = false;
                        }
                        int endPos = str.IndexOf("#");
                        Console.WriteLine("Client String: " + str);
                        if (str.Contains("$"))
                        {
                            int halfPos = str.IndexOf("$");
                            for (int i = 0; i < halfPos; i++)
                            {
                                tempScore1[i] = str.ElementAt(i);

                            }
                            GlobalClient.player1score = new String(tempScore1);
                            for (int j = halfPos+1; j < endPos; j++)
                            {
                                    tempScore2[j-(halfPos+1)] = str.ElementAt(j);
                            }
                            Console.WriteLine(new String(tempScore2) + "temp");
                            Console.WriteLine(GlobalClient.player2score + "global");

                            //for (int k = halfPos + 1; j < endPos; j++)
                            //{
                            //    tempScore2[j - (halfPos + 1)] = str.ElementAt(j);
                            //}
                            //if (new String(tempScore2).Equals(GlobalClient.player2score))
                            //{
                            //    Console.WriteLine("<<<<<<<<<<<in retn dan if la>>>>>>>>>>>>");
                            //    string message = "Word does not exist";
                            //    MessageBox.Show(message);
                            //}
                            GlobalClient.player2score = new String(tempScore2);
                            
                        }
                        else if (str.Contains("!Invalid!"))
                        {
                            Console.WriteLine("<<<<<<<<<<<in retn dan if la>>>>>>>>>>>>");
                            string message = "Word does not exist";
                            MessageBox.Show(message);

                        }
                        else if (str.Contains("!Reset!"))
                        {
                            control = 1;
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
                            Console.WriteLine("Client MSG: " + msg);
                        }

                    }        
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n" + ex.HelpLink + "\n" + ex.InnerException
                            + "\n" + ex.Source + "\n" + ex.TargetSite);
                } 
            }
            socketReceive.Close();
            control = 0;
            this.Dispose(true);
            
            if (GlobalClient.roundPlayed < 3)
            {
                GlobalClient.roundPlayed++;
                Console.WriteLine("Global count: " + GlobalClient.roundPlayed);
                Form2 frm = new Form2();
                frm.Player1ScoreLabel.Text = GlobalClient.player1score;
                frm.Player2ScoreLabel.Text = GlobalClient.player2score;
                frm.ShowDialog();
                
                
            }
            else
            {
               
                Form frm = new Form();
                frm.ShowDialog();
            }
            
            this.Close();
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
            btnNewRound.Enabled = false;
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
            string messageTextBox = word.Text;
            byte[] messageSentFromClient;
            try
            {
                // Retrive the Name of HOST
                string hostName = Dns.GetHostName();
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                socketSend.Connect(iPEndPointSend);
                messageSentFromClient = Encoding.ASCII.GetBytes(messageTextBox + "$" + myIP + "#");
                socketSend.Send(messageSentFromClient, SocketFlags.None);
                btnNewRound.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n" + ex.HelpLink + "\n" + ex.InnerException
                        + "\n" + ex.Source + "\n" + ex.TargetSite);
            }
            finally
            {
                socketSend.Close();
                Submit.Enabled = false;
                TextClear.Enabled = false;
            }
        }

        private void TextClear_Click(object sender, EventArgs e)
        {
            word.Text = string.Empty;
            button10_TextChanged(sender,e);
        }

        private void btnNewRound_Click(object sender, EventArgs e)
        {
            int portSend = 40000;
            IPEndPoint iPEndPointSend = new IPEndPoint(IPAddress.Parse("10.232.20.230"), portSend);
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string messageTextBox = "!Reset!";
            byte[] messageSentFromClient;
            try
            {
                // Retrive the Name of HOST
                string hostName = Dns.GetHostName();
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                socketSend.Connect(iPEndPointSend);
                messageSentFromClient = Encoding.ASCII.GetBytes(messageTextBox + myIP + "#");
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
    }
}
