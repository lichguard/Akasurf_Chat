using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akasurf_Chat_Client
{
    public partial class Snake_Form : Form
    {

        public delegate void UpdateLocationCallback(int x, int y);
        public Form_Client parent_form;



        public Snake_Form(Form_Client parent1)
        {
            InitializeComponent();
            parent_form = parent1;
        }

        private void Snake_Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            parent_form.swSender.WriteLine("incoming");
            parent_form.swSender.Flush();
            parent_form.swSender.WriteLine("SnakeGame");
            parent_form.swSender.Flush();
            parent_form.swSender.WriteLine("updateloc");
            parent_form.swSender.Flush();
            parent_form.swSender.WriteLine(e.KeyChar.ToString());
            parent_form.swSender.Flush();
        

           // MessageBox.Show(e.KeyChar.ToString());
        }

        public void UpdateLocation_Local(int x, int y)
        {
            
            Snake_Head.Left = x;
            Snake_Head.Top = y;
        }

        public void UpdateLocation_Enemy(int x, int y)
        {
            Snake_Head2.Left = x;
            Snake_Head2.Top = y;       
        }

        private void Snake_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent_form.Show();
            parent_form.WindowState = FormWindowState.Normal;

            //parent_form.swSender.WriteLine("incoming");
            //parent_form.swSender.Flush();
            //parent_form.swSender.WriteLine("MinewsweeperGame");
            //parent_form.swSender.Flush();
            //parent_form.swSender.WriteLine("endgame");
            //parent_form.swSender.Flush();
            //parent_form.swSender.WriteLine("your partner left the game");
            //parent_form.swSender.Flush();
            //playing = false;
        }


    }
}
