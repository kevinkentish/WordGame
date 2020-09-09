using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Client_
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Player1Name.Text = GlobalClient.player1Name;
            Player2Name.Text = GlobalClient.player2Name;
            Player1Score.Text = GlobalClient.player1score;
            Player2Score.Text = GlobalClient.player2score;

            if (GlobalClient.player1)
            {
                if (Convert.ToInt32(GlobalClient.player1score) > Convert.ToInt32(GlobalClient.player2score))
                {
                    Results.Text = "Congrats " + GlobalClient.player1Name + ", you won";
                }
                else if (Convert.ToInt32(GlobalClient.player1score) < Convert.ToInt32(GlobalClient.player2score))
                {
                    Results.Text = "Sorry " + GlobalClient.player1Name + ", you suck";
                }
            }
            else
            {
                if (Convert.ToInt32(GlobalClient.player1score) < Convert.ToInt32(GlobalClient.player2score))
                {
                    Results.Text = "Congrats " + GlobalClient.player2Name + ", you won";
                }
                else if (Convert.ToInt32(GlobalClient.player1score) > Convert.ToInt32(GlobalClient.player2score))
                {
                    Results.Text = "Sorry " + GlobalClient.player2Name + ", you suck";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int portSend = 40000;
            IPEndPoint iPEndPointSend = new IPEndPoint(IPAddress.Parse("10.232.20.230"), portSend);
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string messageTextBox = "RestartServer";
            byte[] messageSentFromClient;
            try
            {
                // Retrive the Name of HOST
                string hostName = Dns.GetHostName();
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                socketSend.Connect(iPEndPointSend);
                messageSentFromClient = Encoding.ASCII.GetBytes(messageTextBox + myIP+"#");
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
