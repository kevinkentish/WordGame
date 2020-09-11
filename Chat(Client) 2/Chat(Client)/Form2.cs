using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Chat_Client_
{
    public partial class Form2 : Form
    {
        public static int control = 0;
        public Form2()
        {
            InitializeComponent();

            //Check if client is player 1 and enable playe first 
            if (GlobalClient.player1)
            {
                Vowel.Enabled = true;
                Consonant.Enabled = true;
            }

            TextClear.Enabled = false;
            Submit.Enabled = false;
            Player1Name.Text = GlobalClient.player1Name;
            Player2Name.Text = GlobalClient.player2Name;
            label3.Text = (GlobalClient.roundPlayed + 1).ToString();
            Player1Name.Focus();

            //Create new thread
            CheckForIllegalCrossThreadCalls = false;
            threadReceive = new Thread(new ThreadStart(ReceivedByClient));
            threadReceive.Start();
        }
        //============================================================Receive================================================================================
        Thread threadReceive;
        private void Consonant_Click(object sender, EventArgs e)
        {

            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string messageTextBox = "consonant";
            byte[] messageSentFromClient;
            try
            {
                // Retrive the Name of HOST
                string hostName = Dns.GetHostName();
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                socketSend.Connect(GlobalClient.iPEndPointSend);
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

                Player1Name.Focus();
                socketSend.Close();
            }
        }

        private void Vowel_Click(object sender, EventArgs e)
        {
           
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string messageTextBox = "vowel";
            byte[] messageSentFromClient;
            try
            {
                // Retrive the Name of HOST
                string hostName = Dns.GetHostName();
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                socketSend.Connect(GlobalClient.iPEndPointSend);
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
                Player1Name.Focus();
            }
        }
        public void ReceivedByClient()
        {
            if (control == 1)
            {
                control = 0;
            }
            Socket socketReceive = CreateSocketClient.ReceiveSocket();

            string msg = "??????????";
            string temporaryString = "";
            char[] ch = msg.ToCharArray();
            


            while (control==0)
            {
                Socket temp = null;
                Player1ScoreLabel.Text = GlobalClient.player1score;
                Player2ScoreLabel.Text = GlobalClient.player2score;
                try
                {

                    if (control == 0)
                    {
                        temp = socketReceive.Accept();

                        byte[] messageReceivedByServer = new byte[100];
                        int sizeOfReceivedMessage = temp.Receive(messageReceivedByServer, SocketFlags.None);
                        string str = Encoding.ASCII.GetString(messageReceivedByServer);

                        int endPos = str.IndexOf("#");

                        //Enable buttons when receive !play!
                        if (str.Contains("!play!"))
                        {
                            Consonant.Enabled = true;
                            Vowel.Enabled = true;                         
                        }

                        //Disable buttons when receive !wait!
                        else if (str.Contains("!wait!"))
                        {
                            Consonant.Enabled = false;
                            Vowel.Enabled = false;
                        }                       

                        //Set Players score in global variable
                        if (str.Contains("$"))
                        {
                            btnNewRound.Enabled = true;
                            ExtractMessage.SetScores(str, endPos);                         
                        }

                        //Inform player that word does not exist
                        else if (str.Contains("!Invalid!"))
                        {
                            btnNewRound.Enabled = false;
                            string message = "Word does not exist!! You have been awarded 0 Marks";
                            DialogResult d = MessageBox.Show(message);

                        }

                        //Play continues until receive !Reset!
                        else if (str.Contains("!Reset!"))
                        {
                            control = 1;
                        }

                        else if (str.Contains("!DisplayScore!"))
                        {
                            socketReceive.Close();
                            this.Close();
                            Form3 frm = new Form3();
                            frm.ShowDialog();
                        }

                        else
                        {
                            //Take each character in received string put in array of char.
                            for (int i = 0; i < endPos; i++)
                            {
                                ch[i] = str.ElementAt(i);
                            }

                            //Take array of chars, put in string
                            temporaryString = new String(ch);

                            //Place all characters button in an array
                            Button[] arrbtn = new Button[10] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10 };

                            //Place characters in each button
                            for (int i =0; i<(arrbtn.Length); i++)
                            {
                                arrbtn[i].Text = temporaryString.ElementAt(i).ToString();
                            }
                           
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
            
            //Check is last round reached
            if (GlobalClient.roundPlayed < 4)
            {
                //Reopen Form2 until last round is reached
                GlobalClient.roundPlayed++;
                Form2 frm = new Form2();
                frm.Player1ScoreLabel.Text = GlobalClient.player1score;
                frm.Player2ScoreLabel.Text = GlobalClient.player2score;
                if (GlobalClient.roundPlayed == 4)
                {
                    frm.btnNewRound.Text = "End Game!";
                }
                frm.ShowDialog();
                
            }
            else
            {
                Form3 frm = new Form3();
                frm.ShowDialog();
            }
            
            this.Close();
        }

        private void button10_TextChanged(object sender, EventArgs e)
        {
            //When all characters are picked:
            //Enable submit button 
            //Disable pick consonant, vowel and new round button
            foreach (Button s in Controls.OfType<Button>())
            {
                s.Enabled = true;
                s.UseVisualStyleBackColor = true;
                Consonant.Enabled = false;
                Vowel.Enabled = false;
                btnNewRound.Enabled = false;
            }
            
        }
        void MyButtonClick(object sender, EventArgs e)
        {
            //Pick character and form word
            Button button = sender as Button;
            word.Text += button.Text;
            button.Enabled = false;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
           
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string messageTextBox = word.Text;
            byte[] messageSentFromClient;
            try
            {
                string player = "P2";
                if (GlobalClient.player1)
                {
                    player = "P1";
                }
                // Retrive the Name of HOST
                string hostName = Dns.GetHostName();
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                socketSend.Connect(GlobalClient.iPEndPointSend);
                Console.WriteLine("CLIENT : " + messageTextBox + "$" + myIP + "#" + GlobalClient.roundPlayed);
                messageSentFromClient = Encoding.ASCII.GetBytes(messageTextBox + "$" + myIP + "#" + GlobalClient.roundPlayed+"^"+player);
                
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
            
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string messageTextBox = "!Reset!";
            
            byte[] messageSentFromClient;
            try
            {
                // Retrive the Name of HOST
                string hostName = Dns.GetHostName();
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                socketSend.Connect(GlobalClient.iPEndPointSend);
                messageSentFromClient = Encoding.ASCII.GetBytes(messageTextBox + myIP + "#");
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
