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
    public partial class Minesweeper_Form : Form
    {
        public static int ROWS = 16;
        public static int COLUMS = 16;
        //  public static int BOMBS = 51;
        public static int Local_Score_count = 0;
        public static int Remote_Score_count = 0;
        public static Form_Client parent_form;
        public static string enemey_name;
        public static int local_score;
        public static int remote_score;
        public static bool myturn;
        public static bool playing;
        public static int bombs_left;
        public static bool bomb_active;
        public PictureBox[,] pictures = new PictureBox[ROWS, COLUMS];
        public PictureBox lastopen;
        public Minesweeper_Form(Form_Client parent, string enemey_name1, bool turn)
        {
            InitializeComponent();
            enemey_name = enemey_name1;
            parent_form = parent;
            Build_Bord();
            playing = true;
            bombs_left = 40;
            label_vs.Text = parent_form.UserName + " vs " + enemey_name;
            bomb_active = false;
            if (turn)
            {
                myturn = true;
                Label_Turn.Text = "Your turn";
                Label_Turn.ForeColor = Color.Red;
            }
            else
            {
                myturn = false;
                Label_Turn.Text = enemey_name + "'s turn";
                Label_Turn.ForeColor = Color.Blue;
            }
        }

        private void Build_Bord()
        {
            this.Height = ROWS * 21 + 111;
            this.Width = COLUMS * 22 + 233;
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMS; j++)
                {
                    pictures[i, j] = new System.Windows.Forms.PictureBox();
                    pictures[i, j].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    pictures[i, j].Location = new System.Drawing.Point(210 + i * 22, 68 + j * 21);
                    pictures[i, j].Name = i + "_" + j;
                    pictures[i, j].Size = new System.Drawing.Size(22, 21);
                    pictures[i, j].TabIndex = 15;
                    pictures[i, j].TabStop = false;
                    pictures[i, j].BackColor = Color.Black;
                    pictures[i, j].BackgroundImage = Images_Resource.unopened;
                    pictures[i, j].MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
                    pictures[i, j].MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
                    this.pictures[i, j].MouseClick += new System.Windows.Forms.MouseEventHandler(this.Mouse_clicked);
                    this.Controls.Add(pictures[i, j]);
                }
            }
        }

        private void Mouse_clicked(object sender, MouseEventArgs e)
        {
            int x = int.Parse(((PictureBox)sender).Name.Substring(0, ((PictureBox)sender).Name.LastIndexOf('_')));
            int y = int.Parse(((PictureBox)sender).Name.Substring(((PictureBox)sender).Name.LastIndexOf('_') + 1, ((PictureBox)sender).Name.Length - ((PictureBox)sender).Name.LastIndexOf('_') - 1));
            Send_click(x, y);
        }

        private void Send_click(int x, int y)
        {
            if (!playing)
                return;
            parent_form.swSender.WriteLine("incoming");
            parent_form.swSender.Flush();
            parent_form.swSender.WriteLine("MinewsweeperGame");
            parent_form.swSender.Flush();
            parent_form.swSender.WriteLine("click_pos");
            parent_form.swSender.Flush();
            parent_form.swSender.WriteLine(x.ToString());
            parent_form.swSender.Flush();
            parent_form.swSender.WriteLine(y.ToString());
            parent_form.swSender.Flush();
            parent_form.swSender.WriteLine(bomb_active.ToString());
            parent_form.swSender.Flush();
            if (bomb_active)
            {
                bomb_active = false;
                buttom_bomb.Enabled = false;
                button_cancel_button.Enabled = false;
            }
        }

        public void Update_Image(int x, int y, int data, string turn)
        {
            if (lastopen != null)
                lastopen.BorderStyle = BorderStyle.None;
            lastopen = pictures[x, y];
            lastopen.BorderStyle = BorderStyle.Fixed3D;
            if (turn == "True")
            {
                myturn = true;
                Label_Turn.Text = "Your turn";
                Label_Turn.ForeColor = Color.Red;
            }
            else
            {
                myturn = false;
                Label_Turn.Text = enemey_name + "'s turn";
                Label_Turn.ForeColor = Color.Blue;
            }
            switch (data)
            {
                case 0:
                    pictures[x, y].BackgroundImage = Akasurf_Chat_Client.Images_Resource.empty;
                    break;
                case 1:
                    pictures[x, y].BackgroundImage = Akasurf_Chat_Client.Images_Resource.number_1;
                    break;
                case 2:
                    pictures[x, y].BackgroundImage = Akasurf_Chat_Client.Images_Resource.number_2;
                    break;
                case 3:
                    pictures[x, y].BackgroundImage = Akasurf_Chat_Client.Images_Resource.number_3;
                    break;
                case 4:
                    pictures[x, y].BackgroundImage = Akasurf_Chat_Client.Images_Resource.number_4;
                    break;
                case 5:
                    pictures[x, y].BackgroundImage = Akasurf_Chat_Client.Images_Resource.number_5;
                    break;
                case 10:
                    pictures[x, y].BackgroundImage = Akasurf_Chat_Client.Images_Resource.unopened;
                    break;
                case 11:
                    local_score++;
                    label_local_score.Text = "Your score: " + local_score.ToString();
                    bombs_left--;
                    label_bombs_left.Text = "Bombs left: " + bombs_left.ToString();
                    pictures[x, y].BackgroundImage = Akasurf_Chat_Client.Images_Resource.Flag1;
                    break;
                case 12:
                    remote_score++;
                    bombs_left--;
                    label_bombs_left.Text = "Bombs left: " + bombs_left.ToString();
                    label_remote_score.Text = enemey_name + "'s score: " + remote_score.ToString();
                    pictures[x, y].BackgroundImage = Akasurf_Chat_Client.Images_Resource.Flag2;
                    break;
                default:
                    break;
            }
        }

        public void UpdateLog(string strMessage)
        {
            txtLog.AppendText(strMessage + "\r\n");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!playing)
                return;
            if (txtMessage.Text != "")
            {
                parent_form.swSender.WriteLine("incoming");
                parent_form.swSender.Flush();
                parent_form.swSender.WriteLine("MinesweeperGamechat");
                parent_form.swSender.Flush();
                parent_form.swSender.WriteLine(txtMessage.Text);
                parent_form.swSender.Flush();
                UpdateLog("You: " + txtMessage.Text);
                txtMessage.Text = "";
            }
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (((PictureBox)sender).BackgroundImage == Images_Resource.unopened)
                ((PictureBox)sender).BackgroundImage = Images_Resource.glow_unopened;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (((PictureBox)sender).BackgroundImage == Images_Resource.glow_unopened)
                ((PictureBox)sender).BackgroundImage = Images_Resource.unopened;
        }

        private void Form_Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent_form.Show();
            parent_form.WindowState = FormWindowState.Normal;
            if (!playing)
                return;
            parent_form.swSender.WriteLine("incoming");
            parent_form.swSender.Flush();
            parent_form.swSender.WriteLine("MinewsweeperGame");
            parent_form.swSender.Flush();
            parent_form.swSender.WriteLine("endgame");
            parent_form.swSender.Flush();
            parent_form.swSender.WriteLine("your partner left the game");
            parent_form.swSender.Flush();
            playing = false;
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If the key is Enter 
            if (e.KeyChar == (char)13 && txtMessage.Text != "" && !txtMessage.Text.Contains("\\"))
            {
                e.Handled = true;
                btnSend_Click(sender, new EventArgs());
            }
        }

        private void buttom_bomb_Click(object sender, EventArgs e)
        {
            bomb_active = true;
            button_cancel_button.Enabled = true;
            buttom_bomb.Enabled = false;
        }

        private void button_cancel_button_Click(object sender, EventArgs e)
        {
            bomb_active = false;
            buttom_bomb.Enabled = true;
            button_cancel_button.Enabled = false;
        }


    }
}
