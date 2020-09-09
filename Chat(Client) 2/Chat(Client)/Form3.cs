using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

    }
}
